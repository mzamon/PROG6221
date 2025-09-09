// Form-specific animations and interactions
document.addEventListener('DOMContentLoaded', function () {
    // Animate form fields on focus
    initFormFieldAnimations();

    // Initialize character counters for textareas
    initCharacterCounters();

    // Initialize file upload previews
    initFileUploads();
});

// Form field focus animations
function initFormFieldAnimations() {
    const formFields = document.querySelectorAll('.organic-field input, .organic-field textarea, .organic-field select');

    formFields.forEach(field => {
        // Add focus event
        field.addEventListener('focus', function () {
            this.parentElement.classList.add('focused');
        });

        // Add blur event
        field.addEventListener('blur', function () {
            if (!this.value) {
                this.parentElement.classList.remove('focused');
            }
        });

        // Check initial value
        if (field.value) {
            field.parentElement.classList.add('focused');
        }
    });
}

// Character counters for textareas
function initCharacterCounters() {
    const textareas = document.querySelectorAll('textarea[maxlength]');

    textareas.forEach(textarea => {
        const maxLength = textarea.getAttribute('maxlength');
        const counter = document.createElement('div');
        counter.className = 'character-counter';
        counter.style.cssText = 'text-align:right;font-size:12px;color:#64b5f6;margin-top:5px;';

        textarea.parentNode.appendChild(counter);

        // Update counter on input
        textarea.addEventListener('input', function () {
            const remaining = maxLength - this.value.length;
            counter.textContent = `${remaining} characters remaining`;

            if (remaining < 10) {
                counter.style.color = '#ff7043';
            } else {
                counter.style.color = '#64b5f6';
            }
        });

        // Trigger initial update
        textarea.dispatchEvent(new Event('input'));
    });
}

// File upload preview functionality
function initFileUploads() {
    const fileInputs = document.querySelectorAll('input[type="file"]');

    fileInputs.forEach(input => {
        const previewContainer = document.createElement('div');
        previewContainer.className = 'file-preview';
        previewContainer.style.cssText = 'margin-top:10px;display:none;';

        input.parentNode.appendChild(previewContainer);

        input.addEventListener('change', function (e) {
            const file = this.files[0];
            previewContainer.style.display = 'none';
            previewContainer.innerHTML = '';

            if (file) {
                previewContainer.style.display = 'block';

                if (file.type.startsWith('image/')) {
                    // Image preview
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        const img = document.createElement('img');
                        img.src = e.target.result;
                        img.style.maxWidth = '100%';
                        img.style.maxHeight = '150px';
                        img.style.borderRadius = '8px';
                        previewContainer.appendChild(img);
                    };
                    reader.readAsDataURL(file);
                } else {
                    // Document preview
                    const docInfo = document.createElement('div');
                    docInfo.innerHTML = `
                        <div style="display:flex;align-items:center;gap:10px;padding:10px;background:rgba(33,150,243,0.1);border-radius:8px;">
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="#2196f3">
                                <path d="M14 2H6c-1.1 0-1.99.9-1.99 2L4 20c0 1.1.89 2 1.99 2H18c1.1 0 2-.9 2-2V8l-6-6zm2 16H8v-2h8v2zm0-4H8v-2h8v2zm-3-5V3.5L18.5 9H13z"/>
                            </svg>
                            <div>
                                <div style="font-weight:500;">${file.name}</div>
                                <div style="font-size:12px;color:#64b5f6;">${formatFileSize(file.size)}</div>
                            </div>
                        </div>
                    `;
                    previewContainer.appendChild(docInfo);
                }
            }
        });
    });
}

// Helper function to format file size
function formatFileSize(bytes) {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
}