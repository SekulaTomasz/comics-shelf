import React, { FunctionComponent, useContext, useEffect, useState } from 'react'
import { useQuery } from 'react-query'
import { ComicsListType } from '../../enums/ComicsListType'
import { useAlert } from '../../hooks/UseAlert'
import { IAlertProps } from '../../contexts/AlertContext';
import Spinner from '../Spinner/Spinner'
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button } from '@material-ui/core';
import { IComics } from '../../interfaces/IComics';
import { language } from '../../shared';
import { useSpinner } from '../../hooks/UseSpinner';
import { UserContext } from '../../contexts/UserContext';
import styled from 'styled-components';
import ComicsDetails from '../ComicsDetails/ComicsDetails';
import PurchaseComicsService from '../../api/PurchaseComicsService';

const StyledButtonsContainer = styled.div`
    width: 100%;
    display: flex;
    justify-content: flex-end;
    margin-top: 1rem;
    margin-bottom: 1rem;
`

interface IComicsListProps {
    comicsListType: ComicsListType
}


const ComicsList: FunctionComponent<IComicsListProps> = ({ comicsListType }) => {

    const { toggleAlert } = useAlert();
    const { toggleSpinner } = useSpinner();
    const { user } = useContext(UserContext);

    const [page, setPage] = useState(1);
    const [isOpen, toggleModal] = useState(false);
    const [selectedComics, setComics] = useState<IComics | null>(null)
    
    const { isLoading, isError, data, error, refetch } = useQuery(['posts', comicsListType], async () => {
        toggleSpinner();
        const fetchType = comicsListType == ComicsListType.allAvailable ? `avaliable?currentPage=${page}` : `users?userId=${user?.id}`;
        const path = `${process.env.REACT_APP_API_BASE_URL}/comics/${fetchType}`
        const res = await fetch(path);
        toggleSpinner(false)
        return res.json();
    }, { refetchIntervalInBackground: false,refetchOnWindowFocus: false})

    useEffect(() => {
        refetch();
    },[page])

    useEffect(() => {
        if(selectedComics == null) return;
        toggleModal(true);
    },[selectedComics])
    
    if (isLoading) return <></>

    if (isError) {
        toggleAlert({ isVisible: true, message: error, severity: "error" } as IAlertProps);
        return <></>
    }
    
    if(!data.result || data.result.length <= 0) return <></>


    


    const renderPagination = () => {
        if(!data.result.results) return null;
        const isNextPage = (data.result.currentPage*data.result.pageSize) < data.result.rowCount;
        const isBackPage = data.result.currentPage > 1;
        return (
            <StyledButtonsContainer>
                {isBackPage && <Button variant="contained" onClick={() => {
                    setPage((prev) => prev-1)
                }}>{language.pl.table.goToPreviusPage}</Button>}
                {isNextPage && <Button variant="contained" onClick={() => {setPage((prev) => prev+1)}}>{language.pl.table.goToNextPage}</Button>}
            </StyledButtonsContainer>
        )
    }

    const onDetailsButtonClick = (comics: IComics) => {
        setComics(comics);
    }

    const onPurchaseButtonClick = async(comics: IComics, asExclusive: boolean = false) => {
        try{
            toggleSpinner();
            if(!user) return;
            await PurchaseComicsService.purchaseComics(comics, user.id, asExclusive).catch((ex) => {throw ex});
        } catch(ex){
            toggleAlert({ isVisible: true, message: ex.message, severity: "error" } as IAlertProps);
        }
        finally{
            toggleSpinner(false);
            refetch();
        }
    }

    const onReturnButtonClick = async(comics: IComics) => {
        try{
            toggleSpinner();
            if(!user) return;
            await PurchaseComicsService.returnComicsAsync(user.id,comics.id).catch((ex) => {throw ex});
        } catch(ex){
            toggleAlert({ isVisible: true, message: ex.message, severity: "error" } as IAlertProps);
        }
        finally{
            toggleSpinner(false);
            refetch();
        }
    }

    const renderData = () => {
        if(comicsListType == ComicsListType.allAvailable){
            return data.result.results.map((row: IComics,index: number) => (
                <TableRow key={row.diamondId+`${index}`}>
                    <TableCell component="th" scope="row">
                        {row.title}
                    </TableCell>
                    <TableCell>{row.release_date.toString().split('T')[0]}</TableCell>
                    <TableCell><Button variant="contained" onClick={() => {
                        onDetailsButtonClick(row);
                    }}>{language.pl.table.actionButton}</Button>
                    <Button variant="contained" onClick={() => {onPurchaseButtonClick(row)}}>Wymień</Button>
                    <Button variant="contained" onClick={() => {onPurchaseButtonClick(row, true)}}>Wymień na wyłączność</Button>
                    </TableCell>
                    
                </TableRow>
            ))
        } else {
            return data.result.map((row: any) => (
                <TableRow key={row.diamondId}>
                    <TableCell component="th" scope="row">
                        {row.title}
                    </TableCell>
                    <TableCell>{row.releaseDate.toString().split('T')[0]}</TableCell>
                    <TableCell><Button variant="contained" onClick={() => {
                        onDetailsButtonClick(row);
                    }}>{language.pl.table.actionButton}</Button>
                    <Button variant="contained" onClick={() => {onReturnButtonClick(row)}}>Zwróc</Button>
                    </TableCell>
                </TableRow>
            ))
        }
    }
    
    return (
        <>
            <TableContainer component={Paper}>
                <Table size="small">
                    <TableHead>
                        <TableRow>
                            <TableCell>{language.pl.table.columnsName[0]}</TableCell>
                            <TableCell>{language.pl.table.columnsName[1]}</TableCell>
                            <TableCell>{language.pl.table.columnsName[2]}</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {renderData()}
                    </TableBody>
                </Table>
                {renderPagination()}
                
            </TableContainer>
            <ComicsDetails isModalOpen={isOpen} toggleModal={toggleModal} comics={selectedComics} currentUser={user}/>
        </>
    )
}

export default ComicsList
