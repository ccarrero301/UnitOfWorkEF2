SELECT * FROM Blogs blog WHERE blog.Id = 1

SELECT * FROM Posts post WHERE post.BlogId = 1

SELECT		comm.* 
FROM		Comments comm

INNER JOIN	Posts post
ON			comm.PostId = post.Id

WHERE		post.BlogId = 1

SELECT		*
FROM		Users