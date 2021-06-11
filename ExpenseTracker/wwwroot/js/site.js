
const form = document.getElementById('form');
const btnExpense = document.getElementById('btn-expense');
const addForm = document.getElementById('add');
const expenseVal = document.getElementById('expense');
const valueVal = document.getElementById('value');
addForm.style.display = "none";
function closeForm() {
    addForm.style.display = "none";
};

function showExpenseForm() {
    if (addForm.style.display === "none") {
        addForm.style.display = "block";
    } else {
        addForm.style.display = "none";
    }
}

function sendRequest() {
    let url = '/Expense/CreateExpense';
    let xhr = new XMLHttpRequest()
    xhr.open("POST", url, true)
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')

    let body = JSON.stringify({ expenseFrom: expenseVal.value, value: valueVal.value });
    xhr.send(body);

    form.reset();
};


