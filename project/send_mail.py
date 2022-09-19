import smtplib
from email.mime.text import MIMEText
from email.header import Header

import  random
def  generate_verification_code_v2():
     code_list  =  []
     for  i  in  range ( 2 ):
         random_num  =  random.randint( 0 ,  9 )
         a  =  random.randint( 65 ,  90 )
         b  =  random.randint( 97 ,  122 )
         random_uppercase_letter  =  chr (a)
         random_lowercase_letter  =  chr (b)
         code_list.append( str (random_num))
         code_list.append(random_uppercase_letter)
         code_list.append(random_lowercase_letter)
     verification_code  =  ''.join(code_list)
     return  verification_code
 
def confirm_email(to_user:str):
    mail_host="smtp.qq.com"
    mail_user="1538773813@qq.com"   #server user name(not the email name)
    mail_pass="kledfqajepraiifb"    #server Authentication Code (not the password of email. if your smtp server need, it will give this code to you)
    
    
    sender = 'huangzhe2222@qq.com'
    receivers = [to_user]  

    vercode=generate_verification_code_v2()
    
    message = MIMEText('verification code:'+vercode, 'plain', 'utf-8')
    message['From'] = Header("Jack Huang", 'utf-8')
    message['To'] =  Header("no reply", 'utf-8')
    
    subject = 'verification code'
    message['Subject'] = Header(subject, 'utf-8')
    
    
    try:
        smtpObj = smtplib.SMTP() 
        smtpObj.connect(mail_host, 25)    # 25 is port of my smtp, it may be different for you 
        smtpObj.login(mail_user,mail_pass)  
        smtpObj.sendmail(sender, receivers, message.as_string())
        return (vercode)
    except smtplib.SMTPException:
        return "False"

