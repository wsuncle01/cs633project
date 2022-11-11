import fastapi as FASTAPI
from fastapi import Depends, security
from sqlalchemy.orm import Session
import email_validator as _email_check
import passlib.hash as _hash
import jwt as _jwt

from models import Base, Question, Choice, User
import schema
from main import get_db

# Authentication

_JWT_SECRET = "mysecret"
oauth2schema = security.OAuth2PasswordBearer()

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
	user_obj = User(email=email, hashed_password=hashed_password)

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

def get_current_user(db: Session = Depends(get_db), token: str=Depends())








# Question

def create_question(db: Session, question: schema.QuestionCreate):
	obj = Question(**question.dict())
	db.add(obj)
	db.commit()
	return obj

def get_all_questions(db: Session):
	return db.query(Question).all()

def get_question(db:Session, qid):
	return db.query(Question).filter(Question.id == qid).first()

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

def update_vote(choice_id: int, db:Session):
	obj = db.query(Choice).filter(Choice.id == choice_id).first()
	obj.votes += 1
	db.commit()
	return obj