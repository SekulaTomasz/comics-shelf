import React, { useContext } from 'react';
import { UserContext } from './contexts/UserContext';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import styled from 'styled-components';

const StyledAppContainer = styled.div`
  margin: 0;
  height: 100vh;
`

function App() {



  const { user } = useContext(UserContext);

  return (
    <StyledAppContainer>
      {/* <HomePage /> */}
        {user ? <HomePage /> : <LoginPage />}
    </StyledAppContainer>
  );
}

export default App;
