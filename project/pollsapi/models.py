from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, String, Integer, DateTime, ForeignKey
from sqlalchemy.orm import relationship
from sqlalchemy.sql import func
import passlib.hash as hash

from database import Base


class Question(Base):
	__tablename__ = "question"
	id = Column(Integer, primary_key=True)
	owner_id = Column(Integer, ForeignKey("users.id"))
	role = Column(Integer)
	question_text = Column(String(200))
	pub_date = Column(DateTime, server_default = func.now())

	choices = relationship('Choice', back_populates='question')
	owner = relationship('User', back_populates='posts')


class Choice(Base):
	__tablename__ = "choice"
	id = Column(Integer, primary_key=True)
	question_id = Column(Integer, ForeignKey('question.id', ondelete='CASCADE'))
	choice_text = Column(String(200))
	votes = Column(Integer, default=0)

	question = relationship("Question", back_populates="choices")

class User(Base):
	__tablename__ = "users"
	id = Column(Integer, primary_key = True, index = True)
	role = Column(Integer)
	email = Column(String(20), unique = True, index = True)
	hashed_password = Column(String(1000))
	date_created = Column(DateTime, server_default = func.now())

	posts = relationship('Question', back_populates='owner')

	def verify_password(self, password:str):
		return hash.bcrypt.verify(password, self.hashed_password)

class Answered(Base):
	__tablename__ = "answered"
	id = Column(Integer, primary_key = True)
	question_id = Column(Integer, ForeignKey('question.id', ondelete='CASCADE'))
	user_id = Column(Integer, ForeignKey('users.id'))



