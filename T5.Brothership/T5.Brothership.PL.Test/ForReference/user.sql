select u.ID, u.UserName, ul.Password, u.Email, u.Bio, u.DateJoined, u.DOB, u.Gender, n.Description as Nationality, ut.Description as UserType from "User" u
join UserLogin ul
on ul.UserID = u.ID
join Nationality n
on u.NationalityID=n.ID
join UserType ut
on u.UserTypeID = ut.ID