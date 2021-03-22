import { AppBar,  Tab, Tabs } from '@material-ui/core'
import { language } from '../shared/index';
import React from 'react'
import TabPanel from '../components/TabPanel/TabPanel';
import ComicsList from '../components/ComicsList/ComicsList';
import { ComicsListType } from '../enums/ComicsListType';
import SearchInput from '../components/Search/SearchInput';

const HomePage = () => {

    const [value, setValue] = React.useState(0);

    const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
      setValue(newValue);
    };


    return (
        <>
        <SearchInput />
            <AppBar position="static">
                <Tabs value={value} onChange={handleChange} indicatorColor="primary"
          variant="fullWidth">
                    <Tab label={language.pl.tabs.tabFirst.label}  />
                    <Tab label={language.pl.tabs.tabSecond.label} />
                </Tabs>
                <TabPanel value={value}/>
            </AppBar>
        </>
    )
}

export default HomePage
