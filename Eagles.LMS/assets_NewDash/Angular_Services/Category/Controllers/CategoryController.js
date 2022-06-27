angular.module("InventoryApp_Controllers").controller("categoryCtrl",
    ["$scope", "NotificationServices", "categoryFactory", "DataTableServices",
        (s, n, categoryFactory, dT) => {
            // get all category
            categoryFactory.GetAllCategories().then(function success(response) {
                s.Categories = response.data;
                dT.pushDataTable("#table_Stores");
            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });
            // get propertis of category
            s.openCategoryProperty = (category) => {
         
                categoryFactory.GetAllCategoryProperties(category.Id).then(function success(response) {
                    s.categoryName = category.Name;
                    s.CategoryPropeties = response.data.Properts;
                    $("#modalCategoryProperties").modal();
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            
            }

            // remove category
            s.Remove = (categoryId, index, e) => {
                e.preventDefault();
                bootbox.confirm('Confirm Delete Operation', function (flag) {
                    if (flag) {
                        categoryFactory.RemoveCategory(categoryId).then(function success(response) {
                            s.Categories.splice(index, 1);
                            n.pushNotification("success", "Successfully Deleted");
                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    }
                });
            };
        }]);