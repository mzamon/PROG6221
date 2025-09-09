// Global site functionality
document.addEventListener('DOMContentLoaded', function () {
    // Initialize password visibility toggles
    initPasswordToggles();

    // Initialize form animations
    initFormAnimations();

    // Initialize navigation for dashboard
    initDashboardNavigation();

    // Initialize floating leaves
    initFloatingLeaves();
});

// Password visibility toggle functionality
function initPasswordToggles() {
    const toggleButtons = document.querySelectorAll('.nature-toggle');

    toggleButtons.forEach(button => {
        button.addEventListener('click', function () {
            const input = this.parentElement.querySelector('input');
            if (input) {
                const type = input.getAttribute('type') === 'password' ? 'text' : 'password';
                input.setAttribute('type', type);
                this.classList.toggle('toggle-visible');
            }
        });
    });
}

// Form submission animations
function initFormAnimations() {
    const forms = document.querySelectorAll('form');

    forms.forEach(form => {
        const submitButton = form.querySelector('.harmony-button, .btn-submit');

        if (submitButton) {
            form.addEventListener('submit', function (e) {
                // Only add loading animation if form is valid
                if (form.checkValidity()) {
                    submitButton.classList.add('loading');

                    // Remove loading after form would have submitted
                    setTimeout(() => {
                        submitButton.classList.remove('loading');
                    }, 2000);
                }
            });
        }
    });
}

// Dashboard navigation functionality
function initDashboardNavigation() {
    const navLinks = document.querySelectorAll('nav a.nav-link, .dashboard-nav a');
    const partialContainer = document.getElementById('partial-container');

    if (navLinks.length > 0 && partialContainer) {
        navLinks.forEach(link => {
            link.addEventListener('click', function (e) {
                e.preventDefault();

                // Show loading state
                partialContainer.innerHTML = '<div style="text-align:center;padding:40px;"><div class="button-growth" style="justify-content:center;"><span class="growing-circle circle-1"></span><span class="growing-circle circle-2"></span><span class="growing-circle circle-3"></span></div><p>Loading...</p></div>';

                fetch(this.href)
                    .then(response => response.text())
                    .then(html => {
                        partialContainer.innerHTML = html;
                        // Re-initialize any interactive elements in the loaded partial
                        initPasswordToggles();
                        initFormAnimations();
                    })
                    .catch(error => {
                        partialContainer.innerHTML = '<div style="color:red;text-align:center;padding:20px;">Error loading content. Please try again.</div>';
                    });
            });
        });
    }
}

// Floating leaf animations
function initFloatingLeaves() {
    const leaves = document.querySelectorAll('.floating-leaf');

    leaves.forEach(leaf => {
        // Randomize animation delay and duration for more natural movement
        const randomDelay = Math.random() * 5;
        const randomDuration = 8 + Math.random() * 4;

        leaf.style.animationDelay = `${randomDelay}s`;
        leaf.style.animationDuration = `${randomDuration}s`;
    });
}

// Helper function to show error messages
function showError(message, element) {
    const errorDiv = document.createElement('div');
    errorDiv.className = 'gentle-error show';
    errorDiv.textContent = message;

    if (element && element.parentNode) {
        element.parentNode.insertBefore(errorDiv, element.nextSibling);
    }
}

// Helper function to remove error messages
function removeErrors() {
    const errors = document.querySelectorAll('.gentle-error');
    errors.forEach(error => error.remove());
}