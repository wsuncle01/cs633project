from fastapi import FastAPI, HTTPException, Response, Depends
import schema
from typing import List
import fastapi.security as _security
from sqlalchemy.orm import Session

import crud
from database import SessionLocal, engine
from models import Base

Base.metadata.create_all(bind=engine)

app = FastAPI()

# Dependency
def get_db():
    try:
        db = SessionLocal()
        yield db
    finally:
        db.close()

## Authentication
@app.post("/api/register")
def create_user(user: schema.UserCreate, db: Session = Depends(get_db)):
	db_user = crud.get_user_by_email(email=user.email, db=db)
	if db_user:
		raise HTTPException(status_code = 400, detail="User with that email already exists")
	#Otherwise create user and return token
	user = crud.create_user(user=user, db=db)
	return crud.create_token(user=user)

@app.post("/api/login")
def generate_token(form_data: _security.OAuth2PasswordRequestForm = Depends(), db:Session = Depends(get_db)):
	user = crud.authenticate_user(email=form_data.username, password=form_data.password, db=db)
	if not user: 
		raise HTTPException(status_code = 401, detail = "Invalid Credentials");

	return crud.create_token(user=user)

@app.get("/api/me", response_model = schema.User)
def get_user(user: schema.User = Depends(crud.get_current_user)):
	return user

## Question

@app.post("/questions/", response_model=schema.QuestionInfo)
def create_question(question: schema.QuestionCreate, db: Session = Depends(get_db)):
	return crud.create_question(db=db, question=question)

@app.get("/questions/", response_model=List[schema.Question])
def get_questions(db: Session = Depends(get_db)):
	return crud.get_all_questions(db=db)

def get_question_obj(db, qid):
	obj = crud.get_question(db=db, qid=qid)
	if obj is None:
		raise HTTPException(status_code=404, detail="Question not found")
	return obj

@app.get("/questions/{qid}", response_model=schema.QuestionInfo)
def get_question(qid: int, db: Session = Depends(get_db)):
	return get_question_obj(db=db, qid=qid)
	
@app.put("/questions/{qid}", response_model=schema.QuestionInfo)
def edit_question(qid: int, question: schema.QuestionCreate, db: Session = Depends(get_db)):
	get_question_obj(db=db, qid=qid)
	obj = crud.edit_question(db=db, qid=qid, question=question)
	return obj

@app.delete("/questions/{qid}")
def delete_question(qid: int, db: Session = Depends(get_db)):
	get_question_obj(db=db, qid=qid)
	crud.delete_question(db=db, qid=qid)
	return {"detail": "Question deleted", "status_code": 204}

#choice

@app.post("/questions/{qid}/choice", response_model=schema.ChoiceList)
def create_choice(qid: int, choice: schema.ChoiceCreate, db: Session = Depends(get_db)):
	get_question_obj(db=db, qid=qid)
	return crud.create_choice(db=db, qid=qid, choice=choice)

@app.put("/choices/{choice_id}/vote", response_model=schema.ChoiceList)
def update_vote(choice_id: int, db: Session = Depends(get_db)):
	return crud.update_vote(choice_id=choice_id, db=db)