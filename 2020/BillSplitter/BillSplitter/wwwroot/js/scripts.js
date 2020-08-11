
function submitBillNameForm() {
    var form = document.getElementById("billNameForm");
    form.submit();
}

function copyJoinLink(id) {
    /* Get the text field */
    var copyText = document.getElementById("JoinLink_" + id);

    /* Select the text field */
    copyText.select();
    copyText.setSelectionRange(0, 99999); /*For mobile devices*/

    /* Copy the text inside the text field */
    document.execCommand("copy");

    /* Alert the copied text */
    alert("Copied the text: " + copyText.value);
}

function submitUpdatePosition(formName) {
    var updateForm = document.getElementById(formName);
    updateForm.submit();
}

