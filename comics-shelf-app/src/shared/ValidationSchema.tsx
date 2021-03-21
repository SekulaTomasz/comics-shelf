import * as yup from 'yup';
import language from './Language';

const loginSchema = yup.object({
    login: yup.string()
        .required(language.pl.loginForm.login.required),
    password: yup.string()
        .required(language.pl.loginForm.password.required),
});


export default loginSchema

  