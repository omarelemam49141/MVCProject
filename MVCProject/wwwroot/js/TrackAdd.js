function clicked() {
    var instructor = $("#SuperSelect").val()
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/InstructorsManage/RemoveTrackAssignedForInstructor/" + instructor,
                method: "GET", // Specify the HTTP method as POST
                success: function (response) {
                 
                        Swal.fire("Done")

                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });                }
            });
        }
    });
}


