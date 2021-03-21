import { Container } from '@material-ui/core';
import React, { useContext } from 'react'
import { useMutation, useQuery } from 'react-query';
import LoginForm from '../components/LoginForm/LoginForm';
import { language } from '../shared/index';
import styled from 'styled-components';
import { UserContext } from '../contexts/UserContext';
import { AlertContext } from '../contexts/AlertContext';
import UserService from '../api/UserService';
import { SpinnerContext } from '../contexts/SpinnerContext';
import { useSpinner } from '../hooks/UseSpinner';

const StyledHeader = styled.h1`
    display: flex;
    justify-content: center;
    margin-top: 0;
`

const StyledContainer = styled(Container)`
    
`

const LoginPage = () => {

    const { setContext } = useContext(UserContext);
    const { toggleAlert } = useContext(AlertContext);
    const { toggleSpinner } = useSpinner();


    const onLoginHandler = async(values: any) => {
        const {login, password} = values;
        toggleSpinner();
        //const result = await UserService.loginUser(login,password);       

    }

    return (
        <StyledContainer maxWidth="sm">
            <StyledHeader>{language.pl.login}</StyledHeader>
            <LoginForm loginHandler={onLoginHandler}/>
        </StyledContainer>
    )
}

export default LoginPage
