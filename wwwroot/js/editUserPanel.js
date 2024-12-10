<script>
    document.querySelectorAll('.editable-cell').forEach(cell => {
        cell.addEventListener('click', function () {
            if (this.querySelector('.edit-button') && !this.querySelector('input')) {
                let currentValue = this.textContent;
                let inputElement = document.createElement('input');
                inputElement.type = 'text';
                inputElement.value = currentValue;
                inputElement.classList.add('edit-input');

                // Додати кнопку збереження
                let saveButton = document.createElement('button');
                saveButton.textContent = 'Save';
                saveButton.classList.add('save-button');
                this.appendChild(inputElement);
                this.appendChild(saveButton);

                saveButton.addEventListener('click', function () {
                    // Заміна значення на введене
                    this.parentElement.textContent = inputElement.value;
                    // Видалити елементи після збереження
                    inputElement.remove();
                    saveButton.remove();
                });
            }
        });
    });
</script>
