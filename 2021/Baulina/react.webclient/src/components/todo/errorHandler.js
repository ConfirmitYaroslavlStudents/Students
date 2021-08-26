import toast from 'react-hot-toast';
import { createBrowserHistory } from 'history';

const history = createBrowserHistory();

function handleError(response) {
    switch (response.status) {
        case 404:{                       
                toast.error("Oops! It looks like there is no such item");
                break;
            }
        default:{
            history.push('/error');
            window.location.reload();
            throw Error(response.message);
        }
    }
};

export default handleError;