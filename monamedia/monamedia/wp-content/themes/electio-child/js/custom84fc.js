var wordMap = {
  "add to cart": "Thêm vào giỏ hàng",
  "all": "Tất cả",
  "days": "Ngày",
  "hours": "Giờ",
  "mins": "Phút",
  "secs": "Giây",
  "read now": "Đọc ngay",
  "add to wishlist": "Yêu thích",
  "ask about products": "Hỏi về sản phẩm",
  "categories": "Danh mục",
  "comments": "Bình luận",
  "add to wishlist": "Yêu thích",
  "trending items": "Xu hướng",
  "today's flash sales": "Khuyến mãi hôm nay",
  "next post": "Bài tiếp theo",
  "prev post": "Bài viết trước",
  "sharet": "Chia sẻ"
};

var regex = new RegExp(Object.keys(wordMap).join("|"), "ig");

document.body.innerHTML = document.body.innerHTML.replace(regex, function(match){
  return wordMap[match.toLowerCase()] || match;
});