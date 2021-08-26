function handleError(response) {
    switch (response.statusCode) {
        case 404:
            {
                throw Error("Oops! It looks like there is no such item");
                break;
            }
        case 400:
        {
                throw Error("Something's wrong... You might want to try one more time");
                break;
        }
        default:{
            throw Error(response.message);
        }
    }
} 