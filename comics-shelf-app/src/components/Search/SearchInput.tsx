import { Container, Input } from '@material-ui/core'
import React, { FunctionComponent, useCallback, useContext, useEffect, useState } from 'react'
import styled from 'styled-components';
import ComicsService from '../../api/ComicsService';
import { UserContext } from '../../contexts/UserContext';
import { IComics } from '../../interfaces/IComics';
import debounce from '../../utils/deboucer';
import ComicsDetails from '../ComicsDetails/ComicsDetails';

const StyledHeader = styled.span`
    display: flex;
    justify-content: center;
    margin-top: 0;
    font-size: 4rem;
`

const StyledContainer = styled(Container)`
    margin-top: 1rem;
    margin-bottom: 1rem;
`

const StyledOptions = styled.div`
    background-color: black;
    color: white;
`



const SearchInput: FunctionComponent = () => {
    

    const [value, setValue] = useState('');
    const { user } = useContext(UserContext);

    const [isOpen, toggleModal] = useState(false);
    const [options, setOptions] = useState({
        isVisible: false,
        options: []
    });
    const [selectedComics, setComics] = useState<IComics | null>(null);

    useEffect(() => {
        if(selectedComics == null) return;
        toggleModal(true);
        setOptions({
            isVisible: false,
            options: []
        })
    },[selectedComics])

    const debouncedOnInputHandler = async(event: any) => {
        const { target: { value } } = event;
        if(value.length <= 0){
            return setValue(value);
        }
        const response = await ComicsService.getComicsByTitle(value);
        const result = await response.json();
        setOptions({
            isVisible: true,
            options: result.result
        });
    }

     const onInputHandler = useCallback(debounce(debouncedOnInputHandler, 600), []);

     const generateAutocompleteOptions = () => {
        if(options.options.length <= 0) {
            return (<div >Nie znaleziono rekordow</div>)
        }

        return options.options.map((option: IComics, index: number) => {
            return (<StyledOptions key={option.diamondId+`${index}`}
                    id={option.diamondId}
                    onClick={() => {
                        setComics(option);
                    }}
                    >
                    {option.title}
                </StyledOptions>)
        })
    }



    return (
        <StyledContainer>
            <StyledHeader>Szukaj</StyledHeader>
            <Input type="search" name="szukaj" placeholder="szukaj" fullWidth={true} color={'primary'} onInput={onInputHandler}/>
            {options.isVisible  && <div className="absolute bg-darkSalmon w-1/2 overflow-y-auto flex flex-col p-2 rounded-b-lg">
                {generateAutocompleteOptions()}
            </div>}

            <ComicsDetails isModalOpen={isOpen} toggleModal={toggleModal} comics={selectedComics} currentUser={user}/>
        </StyledContainer>
    )
}



export default SearchInput
