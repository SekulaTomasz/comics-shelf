import { Button, Modal } from '@material-ui/core';
import React, { FunctionComponent } from 'react';
import { IComics } from '../../interfaces/IComics';
import styled from 'styled-components';
import PurchaseComicsService from '../../api/PurchaseComicsService';
import { IUser } from '../../interfaces/IUser';
import { useAlert } from '../../hooks/UseAlert';
import { IAlertProps } from '../../contexts/AlertContext';

const StyledModalBody = styled.div`
    background-color: white;
    height: 40vh;
    color: black;
    margin: 2rem;
    border: 0px;
    padding: 1rem;
`


interface IComicsDetailsProps {
    comics: IComics | null;
    currentUser: IUser | null;
    toggleModal: Function;
    isModalOpen: boolean;
}

const ComicsDetails: FunctionComponent<IComicsDetailsProps> = (props) => {

    const { toggleAlert } = useAlert();
    let linkRef = React.useRef<any>();

    if(!props.comics || !props.currentUser) return <></>;

    const onDownloadButtonClickHandler = async() => {
        try{
            const result = await PurchaseComicsService.canUserDownload(props.currentUser!.id, props.comics!.id);
            const data = await result.json();
            if(!data.result.canUserDownload) throw {message: "Użytkownik nie posiada dostępu do tego zasobu!"};
            const file = await PurchaseComicsService.downloadFile();
            const fileData = await file.blob()
            if(!linkRef.current) return;
            linkRef.current!.href = window.URL.createObjectURL(fileData);
            linkRef.current!.download = `${props.comics?.title}.pdf`;
            linkRef.current!.click();
        } catch(ex){
            toggleAlert({ isVisible: true, message: ex.message, severity: "error" } as IAlertProps);
        }
    }
    
    const body = (
        <StyledModalBody >
          <h2 id="simple-modal-title">{props.comics.title}</h2>
          <ul>
            <li>Autor: {props.comics.publisher}</li>
            <li>Opis: {props.comics.description}</li>
            <li>Cena: {props.comics.price}</li>
            <li><Button variant="contained" onClick={onDownloadButtonClickHandler}>Pobierz</Button></li>
            </ul>
            <a ref={linkRef} style={{display: 'none'}}/>
        </StyledModalBody>
    );

    return (
        <Modal
            open={props.isModalOpen}
            onClose={() => {props.toggleModal(false)}}
            aria-labelledby="simple-modal-title"
            aria-describedby="simple-modal-description"
        >
            {body}
        </Modal>
    )
}

export default ComicsDetails

