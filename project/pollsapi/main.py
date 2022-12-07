from fastapi import FastAPI, HTTPException, Response, Depends
from fastapi.middleware.cors import CORSMiddleware
import schema
from typing import List
import fastapi.security as _security
from sqlalchemy.orm import Session
from crud import get_db

import crud
from models import Base

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=['*'],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
    expose_headers=["*"]
)

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
def generate_token(user: schema.UserLogin, db:Session = Depends(get_db)):
	user = crud.authenticate_user(email=user.email, password=user.password, db=db)
	if not user: 
		raise HTTPException(status_code = 401, detail = "Invalid Credentials");

	return crud.create_token(user=user)

@app.get("/api/me", response_model = schema.User)
def get_user(user: schema.User = Depends(crud.get_current_user)):
	return user

## Question

@app.post("/questions/", response_model=schema.QuestionInfo)
def create_question(question: schema.QuestionCreate, user: schema.User =  Depends(crud.get_current_user), db: Session = Depends(get_db)):
	return crud.create_question(user=user, db=db, question=question)

@app.get("/questions/", response_model=List[schema.Question])
def get_questions(db: Session = Depends(get_db)):
	return crud.get_all_questions(db=db)

@app.get("/questionsuser/", response_model=List[schema.Question])
def get_user_posts(user: schema.User = Depends(crud.get_current_user), db: Session = Depends(get_db)):
	return crud.get_user_posts(user=user, db=db)

def get_question_obj(db, qid):
	obj = crud.get_question(db=db, qid=qid)
	if obj is None:
		raise HTTPException(status_code=404, detail="Question not found")
	return obj

@app.get("/questions/{qid}", response_model=schema.QuestionInfo)
def get_question(qid: int, db: Session = Depends(get_db), user: schema.User = Depends(crud.get_current_user)):
	return get_question_obj(db=db, qid=qid)

@app.get("/answered/{qid}", response_model=bool)
def has_answered(qid: int, db: Session = Depends(get_db), user: schema.User = Depends(crud.get_current_user)):
	return crud.has_answered(qid=qid, user=user, db=db)
	
@app.put("/questions/{qid}", response_model=schema.QuestionInfo)
def edit_question(qid: int, question: schema.QuestionCreate, db: Session = Depends(get_db), user: schema.User =  Depends(crud.get_current_user)):
	get_question_obj(db=db, qid=qid)
	obj = crud.edit_question(db=db, qid=qid, question=question)
	return obj

@app.delete("/questions/{qid}")
def delete_question(qid: int, db: Session = Depends(get_db), user: schema.User =  Depends(crud.get_current_user)):
	get_question_obj(db=db, qid=qid)
	crud.delete_question(db=db, qid=qid)
	return {"detail": "Question deleted", "status_code": 204}

#choice

@app.post("/questions/{qid}/choice", response_model=schema.ChoiceList)
def create_choice(qid: int, choice: schema.ChoiceCreate, db: Session = Depends(get_db), user: schema.User =  Depends(crud.get_current_user)):
	get_question_obj(db=db, qid=qid)
	return crud.create_choice(db=db, qid=qid, choice=choice)

@app.put("/choices/vote", response_model=schema.ChoiceList)
def update_vote(choice_id: int, db: Session = Depends(get_db), user: schema.User =  Depends(crud.get_current_user)):
	return crud.update_vote(choice_id=choice_id, db=db, user=user)

