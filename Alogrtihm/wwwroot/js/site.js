document.addEventListener('DOMContentLoaded', function () {
    const priceSlider = document.getElementById('priceRangeSlider');
    const minPriceInput = document.getElementById('minPrice');
    const maxPriceInput = document.getElementById('maxPrice');
    const priceRangeMin = document.getElementById('priceRangeMin');
    const priceRangeMax = document.getElementById('priceRangeMax');

    if (priceSlider) {
        noUiSlider.create(priceSlider, {
            start: [window.currentMinPrice, window.currentMaxPrice],
            connect: true,
            step: 50,
            range: {
                'min': window.minPrice,
                'max': window.maxPrice
            }
        });

        priceSlider.noUiSlider.on('update', function (values, handle) {
            const value = Math.floor(values[handle]);
            if (handle === 0) {
                minPriceInput.value = value;
                priceRangeMin.textContent = '$' + value;
            } else {
                maxPriceInput.value = value;
                priceRangeMax.textContent = '$' + value;
            }
        });
    }

    const ratingRange = document.getElementById('ratingRange');
    const ratingValue = document.getElementById('ratingValue');
    const minRatingInput = document.getElementById('minRating');

    if (ratingRange) {
        ratingRange.addEventListener('input', function () {
            ratingValue.textContent = this.value;
            minRatingInput.value = this.value;
        });
    }

    const featureCheckboxes = document.querySelectorAll('.feature-checkbox');
    const featuresInput = document.getElementById('featuresInput');

    if (featureCheckboxes.length > 0) {
        featureCheckboxes.forEach(checkbox => {
            checkbox.addEventListener('change', updateFeaturesInput);
        });
    }

    function updateFeaturesInput() {
        const selectedFeatures = [];
        featureCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                selectedFeatures.push(checkbox.value);
            }
        });
        featuresInput.value = selectedFeatures.join(',');
    }

    const sortBySelect = document.getElementById('sortBy');
    const sortDirectionSelect = document.getElementById('sortDirection');
    const sortAlgorithmSelect = document.getElementById('sortAlgorithm');

    if (sortBySelect) {
        sortBySelect.addEventListener('change', applySort);
    }
    if (sortDirectionSelect) {
        sortDirectionSelect.addEventListener('change', applySort);
    }
    if (sortAlgorithmSelect) {
        sortAlgorithmSelect.addEventListener('change', applySort);
    }

    function applySort() {
        const currentUrl = new URL(window.location.href);
        currentUrl.searchParams.set('sortBy', sortBySelect.value);
        currentUrl.searchParams.set('sortDirection', sortDirectionSelect.value);
        currentUrl.searchParams.set('sortAlgorithm', sortAlgorithmSelect.value);
        currentUrl.searchParams.set('page', 1);
        window.location.href = currentUrl.toString();
    }

    const resetButton = document.getElementById('resetFilters');
    if (resetButton) {
        resetButton.addEventListener('click', function () {
            window.location.href = window.resetFiltersUrl;
        });
    }

    const quickViewButtons = document.querySelectorAll('.quick-view');
    const quickViewModal = new bootstrap.Modal(document.getElementById('productQuickViewModal'));
    const quickViewAddToCartBtn = document.getElementById('quickViewAddToCart');

    quickViewButtons.forEach(button => {
        button.addEventListener('click', function () {
            const productId = this.getAttribute('data-product-id');
            const productCard = this.closest('.product-card');
            
            // Get product details from the card
            const productName = productCard.querySelector('.card-title').textContent;
            const productBrand = productCard.querySelector('.card-subtitle').textContent;
            const productPrice = productCard.querySelector('.price').textContent;
            const productRating = productCard.querySelector('.rating').innerHTML;
            const productFeatures = Array.from(productCard.querySelectorAll('.features .badge')).map(badge => badge.textContent);
            const isInStock = !productCard.querySelector('.out-of-stock-badge');

            // Update modal content
            document.getElementById('quickViewProductName').textContent = productName;
            document.getElementById('quickViewProductBrand').textContent = productBrand;
            document.getElementById('quickViewProductPrice').textContent = productPrice;
            document.getElementById('quickViewProductRating').innerHTML = productRating;

            // Update features
            const featuresContainer = document.getElementById('quickViewProductFeatures');
            featuresContainer.innerHTML = '';
            productFeatures.forEach(feature => {
                const badge = document.createElement('span');
                badge.className = 'badge bg-secondary me-1 mb-1';
                badge.textContent = feature;
                featuresContainer.appendChild(badge);
            });

            // Update availability
            const availabilityEl = document.getElementById('quickViewProductAvailability');
            if (isInStock) {
                availabilityEl.innerHTML = '<span class="badge bg-success">In Stock</span>';
                quickViewAddToCartBtn.classList.remove('disabled');
                quickViewAddToCartBtn.textContent = 'Add to Cart';
            } else {
                availabilityEl.innerHTML = '<span class="badge bg-danger">Out of Stock</span>';
                quickViewAddToCartBtn.classList.add('disabled');
                quickViewAddToCartBtn.textContent = 'Out of Stock';
            }

            // Set product ID for add to cart
            quickViewAddToCartBtn.setAttribute('data-product-id', productId);

            // Show modal with animation
            quickViewModal.show();
        });
    });

    // Quick View Add to Cart
    if (quickViewAddToCartBtn) {
        quickViewAddToCartBtn.addEventListener('click', function() {
            if (this.classList.contains('disabled')) return;
            
            const productId = this.getAttribute('data-product-id');
            console.log(`Adding product ${productId} to cart from quick view`);

            // Show success toast
            const toast = document.createElement('div');
            toast.className = 'position-fixed bottom-0 end-0 p-3';
            toast.style.zIndex = '5';
            toast.innerHTML = `
                <div class="toast show" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header">
                        <strong class="me-auto">Shopping Cart</strong>
                        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                    <div class="toast-body">
                        Product added to cart!
                    </div>
                </div>
            `;
            document.body.appendChild(toast);
            setTimeout(() => {
                toast.remove();
            }, 3000);

            // Close modal
            quickViewModal.hide();
        });
    }

    const filterForm = document.getElementById('filterForm');
    if (filterForm) {
        filterForm.addEventListener('submit', function (e) {
            e.preventDefault();
            const formData = new FormData(filterForm);
            const searchParams = new URLSearchParams();

            for (let [key, value] of formData.entries()) {
                if (value) {
                    searchParams.append(key, value);
                }
            }

            if (sortBySelect) searchParams.set('sortBy', sortBySelect.value);
            if (sortDirectionSelect) searchParams.set('sortDirection', sortDirectionSelect.value);
            if (sortAlgorithmSelect) searchParams.set('sortAlgorithm', sortAlgorithmSelect.value);

            searchParams.set('page', '1');

            window.location.href = `${window.location.pathname}?${searchParams.toString()}`;
        });
    }

    window.changePage = function (page) {
        const currentUrl = new URL(window.location.href);
        currentUrl.searchParams.set('page', page);
        window.location.href = currentUrl.toString();
    };

    // Sorting visualization
    const productGrid = document.querySelector('.product-grid');
    let isSorting = false;

    if (sortAlgorithmSelect) {
        sortAlgorithmSelect.addEventListener('change', function() {
            if (isSorting) return;
            isSorting = true;

            const products = Array.from(productGrid.children);
            const algorithm = this.value;
            const sortBy = document.getElementById('sortBy').value;
            const sortDirection = document.getElementById('sortDirection').value;

            // Add sorting animation class
            productGrid.classList.add('sorting-in-progress');

            // Show algorithm info
            const algorithmInfo = document.createElement('div');
            algorithmInfo.className = 'alert alert-info sorting-info';
            algorithmInfo.innerHTML = `
                <div class="d-flex justify-content-between align-items-center">
                    <div>
                        <strong>Sorting Algorithm:</strong> ${getAlgorithmName(algorithm)}
                        <br>
                        <small>Time Complexity: ${getTimeComplexity(algorithm)}</small>
                    </div>
                    <div class="spinner-border spinner-border-sm" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
            `;
            productGrid.parentElement.insertBefore(algorithmInfo, productGrid);

            // Simulate sorting delay based on algorithm
            const delay = getAlgorithmDelay(algorithm);
            setTimeout(() => {
                // Remove sorting animation
                productGrid.classList.remove('sorting-in-progress');
                algorithmInfo.remove();
                isSorting = false;

                // Apply the sort
                applySort();
            }, delay);
        });
    }

    function getAlgorithmName(algorithm) {
        const names = {
            'quick': 'Quick Sort',
            'merge': 'Merge Sort',
            'bubble': 'Bubble Sort',
            'insertion': 'Insertion Sort',
            'selection': 'Selection Sort',
            'heap': 'Heap Sort',
            'shell': 'Shell Sort'
        };
        return names[algorithm] || algorithm;
    }

    function getTimeComplexity(algorithm) {
        const complexities = {
            'quick': 'O(n log n) average, O(n²) worst',
            'merge': 'O(n log n)',
            'bubble': 'O(n²)',
            'insertion': 'O(n²)',
            'selection': 'O(n²)',
            'heap': 'O(n log n)',
            'shell': 'O(n log² n)'
        };
        return complexities[algorithm] || 'Unknown';
    }

    function getAlgorithmDelay(algorithm) {
        const delays = {
            'quick': 800,
            'merge': 1000,
            'bubble': 1500,
            'insertion': 1200,
            'selection': 1300,
            'heap': 900,
            'shell': 1100
        };
        return delays[algorithm] || 1000;
    }
});
