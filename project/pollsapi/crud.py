import fastapi as FASTAPI
from fastapi import Depends, security
from sqlalchemy.orm import Session
import email_validator as _email_check
import passlib.hash as _hash
import jwt as _jwt
from sqlalchemy import and_

from models import Base, Question, Choice, User, Answered
from database import SessionLocal, engine
import schema

Base.metadata.create_all(bind=engine)

# Dependency
def get_db():
    try:
        db = SessionLocal()
        yield db
    finally:
        db.close()

# Authentication

_JWT_SECRET = "mysecret"
oauth2schema = security.OAuth2PasswordBearer("/api/login")

def get_user_by_email(db: Session, email: str):
	return db.query(User).filter(User.email == email).first()


def create_user(db: Session, user: schema.UserCreate):
	#Checking if email is valid
	try:
		valid = _email_check.validate_email(email=user.email)
		email = valid.email
	except _email_check.EmailNotValidError:
		raise FASTAPI.HTTPException(status_code = 404, detail="Please enter a valid email")

	hashed_password = _hash.bcrypt.hash(user.password)
	user_obj = User(email=email, role=user.role, hashed_password=hashed_password)

	db.add(user_obj)
	db.commit()
	return user_obj

def create_token(user: User):
	user_schema_obj = schema.User.from_orm(user)
	user_dict = user_schema_obj.dict()
	del user_dict["date_created"]

	token = _jwt.encode(user_dict, _JWT_SECRET)

	return dict(access_token=token, token_type = "bearer")

def authenticate_user(email: str, password: str, db: Session):
	user = get_user_by_email(email=email, db=db)
	if not user:
		return False
	if not user.verify_password(password=password):
		return False

	return user

def get_current_user(db: Session = Depends(get_db), token: str=Depends(oauth2schema)):
	try:
		payload = _jwt.decode(token, _JWT_SECRET, algorithms=["HS256"])
		user = db.query(User).get(payload["id"])
	except:
		raise FASTAPI.HTTPException(status_code=401, detail="Invalid email or password")

	return schema.User.from_orm(user)

def get_user_posts(user: schema.User, db: Session):
	posts = db.query(Question).filter_by(owner_id=user.id)
	return list(map(schema.Question.from_orm, posts))

# Question

def create_question(user: schema.User, db: Session, question: schema.QuestionCreate):
	obj1 = Question(**question.dict(), owner_id = user.id, role=user.role)
	db.add(obj1)
	db.commit()
	obj2 = Answered(question_id = obj1.id, user_id = user.id)
	db.add(obj2)
	db.commit()
	return obj1

def get_all_questions(db: Session):
	return db.query(Question).all()

def get_question(db:Session, qid):
	return db.query(Question).filter(Question.id == qid).first()

def has_answered(db:Session, qid: int, user: schema.User):
	obj = db.query(Answered).filter(and_(Answered.question_id == qid, Answered.user_id == user.id)).first()
	if not obj: return False
	else: return True

def edit_question(db: Session, qid, question: schema.QuestionCreate):
	obj = db.query(Question).filter(Question.id == qid).first()
	obj.question_text = question.question_text
	obj.pub_date = question.pub_date
	db.commit()
	return obj

def delete_question(db: Session, qid):
	db.query(Question).filter(Question.id == qid).delete()
	db.commit()

# Choice

def create_choice(db:Session, qid: int, choice: schema.ChoiceCreate):
	obj = Choice(**choice.dict(), question_id=qid)
	db.add(obj)
	db.commit()
	return obj

def update_vote(choice_id: int, db:Session, user: schema.User):
	obj = db.query(Choice).filter(Choice.id == choice_id).first()
	obj.votes += 1
	obj1 = Answered(question_id = obj.question_id, user_id = user.id)
	db.add(obj1)
	db.commit()
	return obj

