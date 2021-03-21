import { Container } from '@material-ui/core';
import React, { useContext } from 'react'
import LoginForm from '../components/LoginForm/LoginForm';
import { language } from '../shared/index';
import styled from 'styled-components';
import { UserContext } from '../contexts/UserContext';
import { IAlertProps } from '../contexts/AlertContext';
import UserService from '../api/UserService';
import { useSpinner } from '../hooks/UseSpinner';
import { useAlert } from '../hooks/UseAlert';

const StyledHeader = styled.span`
    display: flex;
    justify-content: center;
    margin-top: 0;
    font-size: 4rem;
`

const StyledContainer = styled(Container)`
    
`

const LoginPage = () => {

    const { setContext } = useContext(UserContext);
    const { toggleAlert } = useAlert();
    const { toggleSpinner } = useSpinner();


    const onLoginHandler = async(values: any) => {
        const {login, password} = values;
        try {
            toggleSpinner();
        
        const response = await UserService.loginUser(login,password).catch((ex) => {
            throw ex;
        });
        const result = await response.json();
        if(!result.result) throw {message: result.error ?? "Unhandled error!", alertType: "error"};
        setContext(result.result);
        } catch(ex){
            toggleAlert({isVisible: true, message: ex.message, severity: ex.alertType ? ex.alertType : "error"} as IAlertProps)
            
        } finally{
            toggleSpinner(false);
        }
        
    }

    return (
        <StyledContainer maxWidth="sm">
            <StyledHeader>{language.pl.login}</StyledHeader>
            <LoginForm loginHandler={onLoginHandler}/>
        </StyledContainer>
    )
}

export default LoginPage
