SELECT * FROM Blogs blog WHERE blog.Id = 9

SELECT * FROM Posts post WHERE post.BlogId = 9

SELECT		comm.* 
FROM		Comments comm

INNER JOIN	Posts post
ON			comm.PostId = post.Id

WHERE		post.BlogId = 9

SELECT		*
FROM		Users