
const btnExpense = document.getElementById('btn-expense');
const addForm = document.getElementById('add');
const closeBtn = document.getElementById('close');

function addExpense() {
    addForm.innerHTML = '';

    var div = 
        `<form asp-action="CreateExpense" method="post">
        <div asp-validation-summary="ModelOnly" class="text-danger"></div>
        <div class="form-group">
            <label asp-for="ExpenseFrom"></label>
            <input asp-for="ExpenseFrom" class="form-control" />
            <span asp-validation-for="ExpenseFrom" class="text-danger"></span>
        </div>
        <div class="form-group">
            <label asp-for="Value"></label>
            <input asp-for="Value" class="form-control" />
            <span asp-validation-for="Value" class="text-danger"></span>
        </div>
        <div class="form-group">
            <input type="submit" value="submit" class="btn btn-primary" />
              
                <button onclick="closeForm()" class="btn"><i class="fa fa-times"></i></button>
        </div>
    </form>`

    addForm.innerHTML += div;
}

function closeForm() {
    addForm.innerHTML = '';
};

btnExpense.addEventListener('click', function () {
    addExpense();
});
