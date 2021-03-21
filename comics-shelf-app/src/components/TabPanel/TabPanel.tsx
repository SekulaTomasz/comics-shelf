import { Box, Container } from '@material-ui/core';
import React from 'react';
import { ComicsListType } from '../../enums/ComicsListType';
import ComicsList from '../ComicsList/ComicsList';

interface TabPanelProps {
    children?: React.ReactNode;
    value: number;
}
  
const TabPanel = (props: TabPanelProps) => {
    const { children,value, ...other } = props;
  
    return (
      <Container
        role="tabpanel"
        // {...other}
        style={{
          backgroundColor: 'white',
          color: 'black',
          minHeight: '90',
        }}
      >
        <ComicsList comicsListType={value as ComicsListType} key={value}/>
      </Container>
    );
  }

export default TabPanel;