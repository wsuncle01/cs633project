from wtforms import BooleanField, StringField, PasswordField, validators,SubmitField,RadioField
from wtforms.validators import DataRequired,Email,Length,EqualTo
from flask_wtf import FlaskForm

from back_end import user_level

class RegistrationForm(FlaskForm):
    username = StringField(label='Username', validators=[DataRequired()])
    email   = StringField('Email Address', validators=[DataRequired(),Email(message="e-mail form error")])
    passwd1 = PasswordField('New Password', validators=[DataRequired(),
                                                        Length(min=6,max=30,message='password length error')])
    passwd2 = PasswordField('Confirm Password',validators=[DataRequired(),
                                                        Length(min=6,max=30,message='password length error'),
                                                        EqualTo('passwd1',message="passwords are not same")])
    submit  = SubmitField(label='register')

class Loginform(FlaskForm):
    username=StringField(label='Username',validators=[DataRequired()])
    passwd=StringField('Password',validators=[DataRequired()])
    submit=SubmitField(label='login')

class send_message_form(FlaskForm):
    receive_username=StringField(label='To_user',validators=[DataRequired()])
    sendbox=StringField('Sendbox')
    submit=SubmitField(label='send')

class post_events_form(FlaskForm):
    title=StringField(label='Title',validators=[DataRequired()])
    events=StringField(label='Events')
    tags=StringField(label='Tags')
    submit=SubmitField('Post')

class user_level_form(FlaskForm):
    level=RadioField(label='user level',choices=[('1','Students'),('2','Faculties'),('3','External user')])
    submit=SubmitField('submit')