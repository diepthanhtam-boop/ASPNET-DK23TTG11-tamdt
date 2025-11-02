// Fix for cart checkout functionality
document.addEventListener('DOMContentLoaded', function() {
    // Update cart count on page load
    updateCartDisplay();
    
    // Fix checkout button
    const checkoutBtn = document.querySelector('a[href="/Cart/Checkout"]');
    if (checkoutBtn) {
        checkoutBtn.addEventListener('click', function(e) {
            // Check if user is logged in
            const userId = sessionStorage.getItem('UserId');
            if (!userId) {
                e.preventDefault();
                alert('Vui lòng đăng nhập để thanh toán!');
                window.location.href = '/Auth/Login';
                return;
            }
        });
    }
});

function updateCartDisplay() {
    // Update cart count in header
    fetch('/Cart/GetCartCount')
        .then(response => response.json())
        .then(data => {
            const cartCountElements = document.querySelectorAll('#cart-count, #mobile-cart-count');
            cartCountElements.forEach(element => {
                if (element) {
                    element.textContent = data.count || 0;
                }
            });
        })
        .catch(error => console.log('Cart count update failed:', error));
}