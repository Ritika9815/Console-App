// GET: Insuree/Admin
public ActionResult Admin()
{
    var insurees = db.Insurees.ToList();
    return View(insurees);
}
@model IEnumerable<YourProject.Models.Insuree>

<table class="table">
    <thead>
        <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email Address</th>
            <th>Quote</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var insuree in Model)
        {
            <tr>
                <td>@insuree.FirstName</td>
                <td>@insuree.LastName</td>
                <td>@insuree.EmailAddress</td>
                <td>@insuree.Quote.ToString("C")</td>
            </tr>
        }
    </tbody>
</table>
