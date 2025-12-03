window.CustomSelect = class CustomSelect {
    constructor(options) {
        this.input = document.querySelector(options.input);
        this.dropdown = document.querySelector(options.dropdown);
        this.fetchApi = options.fetchApi;

        this.page = 1;
        this.pageSize = options.pageSize || 20;
        this.keyword = "";
        this.isLoading = false;
        this.hasMore = true;

        this.selectedValue = null;

        this.initEvents();
    }

    initEvents() {
        let timer;

        this.input.addEventListener("input", () => {
            clearTimeout(timer);
            timer = setTimeout(() => {
                this.keyword = this.input.value.trim();
                this.load(true);
            }, 300);
        });

        this.input.addEventListener("focus", () => this.load(true));

        document.addEventListener("click", (e) => {
            if (!e.target.closest(".custom-select")) {
                this.hide();
            }
        });

        this.dropdown.addEventListener("scroll", () => {
            if (this.dropdown.scrollTop + this.dropdown.clientHeight >= this.dropdown.scrollHeight - 10) {
                this.load();
            }
        });
    }

    async load(reset = false) {
        if (this.isLoading || (!this.hasMore && !reset)) return;

        this.isLoading = true;

        if (reset) {
            this.dropdown.innerHTML = "";
            this.page = 1;
            this.hasMore = true;
        }

        this.show();

        const res = await this.fetchApi({
            keyword: this.keyword,
            page: this.page,
            pageSize: this.pageSize
        });

        const items = res.result;

        items.forEach(item => {
            const div = document.createElement("div");
            div.className = "dropdown-item";
            div.textContent = `${item.code} - ${item.name}`;
            div.dataset.value = item.id;

            div.addEventListener("click", () => {
                this.select(item);
            });

            this.dropdown.appendChild(div);
        });

        this.hasMore = this.page < res.paginationHeader.totalPages;
        this.page++;

        this.isLoading = false;
    }

    select(item) {
        this.selectedValue = item.id;
        this.input.value = `${item.code} - ${item.name}`;
        this.hide();
    }

    show() {
        this.dropdown.style.display = "block";
    }

    hide() {
        this.dropdown.style.display = "none";
    }

    getValue() {
        return this.selectedValue;
    }
};
