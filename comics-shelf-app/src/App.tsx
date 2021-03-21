import React, { useContext } from 'react';
import { UserContext } from './contexts/UserContext';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import styled from 'styled-components';
import Spinner from './components/Spinner/Spinner';

const StyledAppContainer = styled.div`
  margin: 0;
  height: 100vh;
`

function App() {

  const { user } = useContext(UserContext);

  return (
    <StyledAppContainer>
        {user ? <HomePage /> : <LoginPage />}
    </StyledAppContainer>
  );
}

export default App;
