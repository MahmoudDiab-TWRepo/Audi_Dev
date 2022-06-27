angular.module("InventoryApp_Controllers").controller("categoryCtrl",
    ["$scope", "NotificationServices", "categoryFactory", "DataTableServices",
        (s, n, categoryFactory, dT) => {

            s.CategoryPropeties = [];

            let fullCategoryPropeties = () => {
        
                if (bind() == undefined) {
                    s.CategoryPropeties = [{
                        PropertyName: "",
                        DataType: "String",
                        IsRequired: false,

                    }];
                } else {
     
                    // get from db
                    categoryFactory.GetAllCategoryProperties(bind()).then(function success(response) {
                        s.CategoryPropeties = response.data.Properts;
                        s.Name = response.data.Category.Name; 
                        s.Description = response.data.Category.Description; 


                        s.CategoryPropeties.push({
                            PropertyName: "",
                            DataType: "String",
                            IsRequired: false,
                        });

                    }, function error(error) {
                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });
                }

            }
            fullCategoryPropeties();
            s.addNewProperty = function (index) {
                // check the last record

                if (s.CategoryPropeties.length - 1 == index) {
                    let flag = true;
                    s.CategoryPropeties.forEach(function (arr, index) {
                        if (arr.PropertyName == "" || typeof (arr.PropertyName) == 'undefined') {
                            n.pushNotification("danger", "Please fill in all property first");
                            s.CategoryPropeties[index].IsInValid = true;
                            flag = false;
                            return;
                        } else {
                            s.CategoryPropeties[index].IsInValid = false;
                        }
                    });
                    if (flag) {
                        s.CategoryPropeties.push({
                            PropertyName: "",
                            DataType: "String",
                            IsRequired: false,
                        });
                   
                    }
                }
            }
            // delete record
            s.deleteProperty = function (index,propertyId) {
                bootbox.confirm('Confirm Delete Property', function (flag) {
                    if (flag) {
                        if (propertyId != undefined) {
                     // delete from db
                            categoryFactory.DeleteProperty(propertyId).then(function success(response) {
                                s.CategoryPropeties.splice(index, 1);
                                n.pushNotification("success", "Successfully Deleted");
                            }, function error(error) {

                                let errorMessage = manageAjaxError(error);
                                n.pushNotification("danger", errorMessage);
                            });
                        } else {
                            if (index != 0) {
                                s.CategoryPropeties.splice(index, 1);
                                s.$apply();

                            }
                            else {
                                bootbox.alert('Can Not Delete First Property');
                            }
                        }
                    }
                });
            }
            
            s.EditCategory = function () {
                if (validateForm()) {
                    let data = fullData();
                    if (data != null) {
                        categoryFactory.UpdateCategory(data).then(function success(response) {
                            n.pushNotification("success", "Updated Successfully");
                            fullCategoryPropeties();
                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    }
                }
            };
            s.CreateCategory = function () {
                if (validateForm()) {
                    let data = fullData();
                    if (data != null) {
                        categoryFactory.CreateCategory(data).then(function success(response) {
                            n.pushNotification("success", "Added Successfully");
                            Clear();
                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    }
                }
            };
            
                // clear 
                function Clear() {
                    s.Name = "";
                    s.Description = "";
                    fullCategoryPropeties();
                }
            // validate form
            function validateForm(isEditMode = false) {

                let flag = false;
                if (s.Name == "" || typeof (s.Name) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Category Name");
                }

                else
                    flag = true;

                return flag;
            }
            function fullData() {
                let flag = true;
                let propties = [];
                s.CategoryPropeties.forEach(function (arr, index) {
                    // check the record not null and have a value
                    if (arr.PropertyName != "" && typeof (arr.PropertyName) != 'undefined') {
                        arr.IsInValid = false;
                        propties.push(arr);
                    } else {
                        // skip last record only is empty 
                        if (s.CategoryPropeties.length - 1 != index) {
                            arr.IsInValid = true;
                            n.pushNotification("danger", "Please fill in all property first");
                            flag = false;
                            return;
                        }
                    }
                });
                if (!flag)
                    return null;
                return {
                    Id: bind(),
                    Name: s.Name,
                    Description: s.Description,
                    PropertiesOfCategory: propties
                };
            }
            function bind() {
                return $("#CategoryID").val();
                
            }
        }]);


