## W celu uruchomienia aplikacji na maszynie lokalnej trzeba wykonać następujące kroki:

### Pobranie kodu zródłowego 
`
    git clone https://github.com/SekulaTomasz/comics-shelf.git
`



### Uruchomienie cześci serwerowej:
    Wymagania: 
        .net core 3.1
        lokalna baza danych mssqsl

    Podmiana w pliku 'appsettings.json' w projekcie 'comics-shelf-api' connection stringa do lokalnej bazy MsSql a następnie uruchomienie projektu. Projekt powinien zostać uruchomiony pod adresem 'https://localhost:44323'


## Uruchomienie cześci klienckiej
    Wymagania: 
        nodejs w wersji 12.4.0

    Po pobraniu projektu trzeba zainstalowac node_nodule,
`
    npm install
`
    
    następnie w przypadku jeśli aplikacja serwerowa uruchomi się na innych porcie lub pod innym adresem niż wskazany powyżej należy podmienić zmieną 'REACT_APP_API_BASE_URL' w pliku '.env';
    Po wykonaniu powyższych kroków można odpalić aplikacje

