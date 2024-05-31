document.addEventListener('DOMContentLoaded', () => {
    const newsContainer = document.getElementById('news-container');
    let page = 2;
    const limit = 6;
    let loading = false; // Flag to track if loading is in progress

    const newsData = [
        /*{ id: 1, image: '/images/bird.png', title: 'News Item 1', body: 'Content for news item 1.' },
        { id: 2, image: '/images/bird.png', title: 'News Item 2', body: 'Content for news item 2.' },
        { id: 3, image: '/images/bird.png', title: 'News Item 3', body: 'Content for news item 3.' },
        { id: 4, image: '/images/bird.png', title: 'News Item 4', body: 'Content for news item 4.' },
        { id: 5, image: '/images/bird.png', title: 'News Item 5', body: 'Content for news item 5.' },
        { id: 6, image: '/images/bird.png', title: 'News Item 6', body: 'Content for news item 6.' },
        { id: 7, image: '/images/bird.png', title: 'News Item 7', body: 'Content for news item 7.' },
        { id: 8, image: '/images/bird.png', title: 'News Item 8', body: 'Content for news item 8.' },
        { id: 9, image: '/images/bird.png', title: 'News Item 9', body: 'Content for news item 9.' },
        { id: 10, image: '/images/bird.png', title: 'News Item 10', body: 'Content for news item 10.' },
        { id: 11, image: '/images/bird.png', title: 'News Item 11', body: 'Content for news item 11.' },
        { id: 12, image: '/images/bird.png', title: 'News Item 12', body: 'Content for news item 12.' },
        { id: 13, image: '/images/bird.png', title: 'News Item 13', body: 'Content for news item 13.' },
        { id: 14, image: '/images/bird.png', title: 'News Item 14', body: 'Content for news item 14.' },
        { id: 15, image: '/images/bird.png', title: 'News Item 15', body: 'Content for news item 15.' },
        { id: 16, image: '/images/bird.png', title: 'News Item 16', body: 'Content for news item 16.' },
        { id: 17, image: '/images/bird.png', title: 'News Item 17', body: 'Content for news item 17.' },
        { id: 18, image: '/images/bird.png', title: 'News Item 18', body: 'Content for news item 18.' },
        { id: 19, image: '/images/bird.png', title: 'News Item 19', body: 'Content for news item 19.' },
        { id: 20, image: '/images/bird.png', title: 'News Item 20', body: 'Content for news item 20.' },
        { id: 21, image: '/images/bird.png', title: 'News Item 21', body: 'Content for news item 21.' },
        { id: 22, image: '/images/bird.png', title: 'News Item 22', body: 'Content for news item 22.' },
        { id: 23, image: '/images/bird.png', title: 'News Item 23', body: 'Content for news item 23.' },
        { id: 24, image: '/images/bird.png', title: 'News Item 24', body: 'Content for news item 24.' },
        { id: 25, image: '/images/bird.png', title: 'News Item 25', body: 'Content for news item 25.' },
        { id: 26, image: '/images/bird.png', title: 'News Item 26', body: 'Content for news item 26.' },
        { id: 27, image: '/images/bird.png', title: 'News Item 27', body: 'Content for news item 27.' },*/
        // Add more items as needed
    ];

    const createNewsItemHTML = (item) => {
        return `
            <div class="news-item" data-id="${item.id}">
                <img src="${item.image}"><img>
                <h2>${item.title}</h2>
                <p>${item.body}</p>
                <a href="#" class="view-detail">View Details</a>
            </div>
        `;
    };

    const loaderHTML = `
    <div id="loader-container" class="loader-container">
        <div id="loader" class="loader" style="display: none;">
            <div class="dot-container">
                <div class="snippet" data-title="dot-spin">
                    <div class="stage">
                        <div class="dot-spin"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    `;

    const displayNews = (news) => {
        news.forEach(item => {
            const newsItemHTML = createNewsItemHTML(item);
            newsContainer.insertAdjacentHTML('beforeend', newsItemHTML);
        });

        const loader = document.getElementById('loader-container');
        if (!loader) {
            newsContainer.insertAdjacentHTML('beforeend', loaderHTML);
        } else {
            newsContainer.appendChild(loader);
        }

        addEventListeners();
    };

    const handleScroll = () => {
        const scrollPosition = window.scrollY;
        const windowHeight = window.innerHeight;
        const documentHeight = document.body.offsetHeight;

        if (scrollPosition + windowHeight >= documentHeight - 100 && !loading) {
            loadNews();
        }
    };

    const loadNews = async () => {
        loading = true;
        const loader = document.getElementById('loader');
        loader.style.display = 'block';
        const news = await fetchNews(page, limit);
        if (news.length === 0) {
            loader.style.display = 'none';
            window.removeEventListener('scroll', handleScroll);
            return;
        }
        displayNews(news);
        page++;
        loading = false;
    };

    const fetchNews = (page, limit) => {
        return new Promise((resolve) => {
            const start = (page - 1) * limit;
            const end = start + limit;
            setTimeout(() => {
                resolve(newsData.slice(start, end));
            }, 1000); // Simulating API request delay
        });
    };

    //const addEventListeners = () => {
    //    const buttons = document.querySelectorAll('.view-detail');
    //    buttons.forEach(button => {
    //        button.addEventListener('click', (e) => {
    //            e.preventDefault();
    //            const newsItem = e.target.closest('.news-item');
    //            const newsId = newsItem.getAttribute('data-id');
    //            // Redirect to news detail page with the news ID
    //            window.location.href = `news-detail.html?id=${newsId}`;
    //        });
    //    });
    //};

    // Initial load of news items
    displayNews(newsData.slice(0, limit));
    window.addEventListener('scroll', handleScroll);
});