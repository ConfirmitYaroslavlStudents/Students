import { createBrowserHistory } from 'history';

const history = createBrowserHistory();

function handleError(response) {
    switch (response.status) {
        case 404:
            {           
                history.push('/notfound');
                window.location.reload();
                throw Error("Oops! It looks like there is no such item");
            }
        default:{
            history.push('/error');
            window.location.reload();
            throw Error(response.message);
        }
    }
};

export default handleError;