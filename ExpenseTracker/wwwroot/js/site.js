const btnExpense = document.getElementById('btn-expense');
const btnIncome = document.getElementById('btn-income');
const incomeForm = document.getElementById('income-form');
const expenseForm = document.getElementById('expense-form');
const incomeSubmitForm = document.getElementById('incomeSubmitForm');
const expenseSubmitForm = document.getElementById('expenseSubmitForm');
const incomeFrom = document.getElementById('income-from');
const incomeValue = document.getElementById('income-value');
const expenseFrom = document.getElementById('expense-from');
const expenseValue = document.getElementById('expense-value');

let check = "";

incomeSubmitForm.style.display = "none";
expenseSubmitForm.style.display = "none";

function closeForm() {
    expenseSubmitForm.style.display = "none";
    incomeSubmitForm.style.display = "none";
    incomeForm.reset();
    expenseForm.reset();
}

function showIncomeSubmitForm() {
    check = "income";

    if (expenseSubmitForm.style.display === "block") {
        expenseSubmitForm.style.display = "none"

    }

    if (incomeSubmitForm.style.display === "none") {
        incomeSubmitForm.style.display = "block";
    } else {
        incomeSubmitForm.style.display = "none";
    }
}

function showExpenseSubmitForm() {
    check = "expense";
    if (incomeSubmitForm.style.display === 'block') {
        incomeSubmitForm.style.display = "none"
    }

    if (expenseSubmitForm.style.display === "none") {
        expenseSubmitForm.style.display = "block";
    } else {
        expenseSubmitForm.style.display = "none";
    }
}

function sendRequest() {
    if (check === "income") {
        let url = '/Income/CreateIncome';
        let xhr = new XMLHttpRequest()
        xhr.open("POST", url, true)
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
        let body = JSON.stringify({ incomeFrom: incomeFrom.value, value: incomeValue.value });
        xhr.send(body);
        incomeForm.reset();
    }

    if (check === "expense") {
        let url = '/Expense/CreateExpense';
        let xhr = new XMLHttpRequest()
        xhr.open("POST", url, true)
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
        let body = JSON.stringify({ expenseFrom: expenseFrom.value, value: expenseValue.value });
        xhr.send(body);
        expenseForm.reset();
    }
}


