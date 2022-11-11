from datetime import datetime

from pydantic import BaseModel
from typing import List

# User schema

class UserBase(BaseModel):
	email: str

class UserCreate(UserBase):
	password: str

	class Config:
		orm_mode = True

class User(UserBase):
	id: int
	date_created: datetime

	class Config:
		orm_mode = True

# Choice schema

class ChoiceBase(BaseModel):
	choice_text: str
	votes: int = 0

class ChoiceCreate(ChoiceBase):
	pass

class ChoiceList(ChoiceBase):
	id: int

	class Config:
		orm_mode = True


# Question schema

class QuestionBase(BaseModel):
	question_text: str

class QuestionCreate(QuestionBase):
	pass

class Question(QuestionBase):
	id: int
	owner_id: int
	pub_date: datetime

	class Config:
		orm_mode = True

class QuestionInfo(Question):
	choices: List[ChoiceList] = []