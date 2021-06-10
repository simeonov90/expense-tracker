
const form = document.getElementById('form');
const btnExpense = document.getElementById('btn-expense');
const addForm = document.getElementById('add');
const expenseVal = document.getElementById('expense');
const valueVal = document.getElementById('value');

function closeForm() {
    addForm.innerHTML = '';
};

function sendRequest() {
    let url = '/Expense/CreateExpense';
    let xhr = new XMLHttpRequest()
    xhr.open("POST", url, true)
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')

    let body = JSON.stringify({ expenseFrom: expenseVal.value, value: valueVal.value });
    xhr.send(body);

    form.reset();
};

btnExpense.addEventListener('click', function () {
    submitForm();
});


