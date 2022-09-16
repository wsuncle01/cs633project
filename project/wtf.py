from wtforms import BooleanField, StringField, PasswordField, validators,SubmitField
from wtforms.validators import DataRequired,Email,Length,EqualTo
from flask_wtf import FlaskForm

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