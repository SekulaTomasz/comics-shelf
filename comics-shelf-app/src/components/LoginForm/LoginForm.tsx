import { Button, Input, TextField } from '@material-ui/core';
import { ErrorMessage, Field, Form, Formik, useFormik } from 'formik';
import React, { FunctionComponent } from 'react'
import { language } from '../../shared';
import loginSchema from '../../shared/ValidationSchema';
import styled from 'styled-components';


const StyledForm = styled.form`
    display: flex;
    flex-direction: column;
`

interface ILoginFormProps {
    loginHandler: Function;
}

const LoginForm: FunctionComponent<ILoginFormProps> = ({ loginHandler }) => {

    const formik = useFormik({
        initialValues: {
            login: '',
            password: ''
        },
        validationSchema: loginSchema,
        onSubmit: (values) => {
            loginHandler(values);
        },
    });

    return (
        <StyledForm onSubmit={formik.handleSubmit}>
            <TextField id="login" name="login" label={language.pl.loginForm.login.placeholder} variant="outlined" style={{ margin: '1rem' }} type="text"
                 value={formik.values.login}
                onChange={formik.handleChange}
                error={formik.touched.login && Boolean(formik.errors.login)}
                helperText={formik.touched.login && formik.errors.login} />

            <TextField id="password" name="password" label={language.pl.loginForm.password.placeholder} variant="outlined" style={{ margin: '1rem' }} type="password" 
                value={formik.values.password}
                onChange={formik.handleChange}
                error={formik.touched.password && Boolean(formik.errors.password)}
                helperText={formik.touched.password && formik.errors.password} />

            <Button variant="contained" color="primary" style={{ margin: '1rem' }} type="submit">{language.pl.loginForm.loginButton}</Button>
        </StyledForm>
    )
}

export default LoginForm
