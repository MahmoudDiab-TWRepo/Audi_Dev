angular.module("InventoryApp_Controllers").controller("itemCtrl",
    ["$scope", "NotificationServices", "ItemsFactory", "DataTableServices", "storesFactory",
        (s, n, itemsFactory, dT, storesFactory) => {



            s.openCategoryProperty = (item) => {
                itemsFactory.GetAllItemPropertiesValues(item.Id).then(function success(response) {
                    s.CategoryPropeties = response.data.Properts;
                    $("#modalCategoryValuesProperties").modal();
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });

            }

            s.openItemMovmentStatuses = (item) => {

                itemsFactory.GetAllItemMovmentStatuses(item.Id).then(function success(response) {
                    s.ItemMovmentStatusesValues = response.data.Statuses;
                    $("#modalItemMovmentStatusesValues").modal();
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });

            }
            s.showEditHistory = (e, itemHistoryId) => {
                e.preventDefault();
                s.ItemEditHistoryies = [];
                //ItemEditHistoryies

                itemsFactory.GetItemEditHistoryies(itemHistoryId).then(function success(response) {
                    if (response.data.length > 0) {
                        s.ItemEditHistoryies = response.data;
                        $("#ItemEditHistoryModal").modal();


                    } else {
                        n.pushNotification("danger", "There is no update in the data");

                    }

                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });

            }
            s.ddl_itemtype = "all";
            s.itemfilter = () => {
                let type = s.ddl_itemtype;

            }

            s.CategoryPropeties = [];
            s.ItemMovmentStatusesValues = s.ItemEditHistoryies = [];
            s.propName = [];
            s.CategoryPropetiesEdit = [];
            var usid = $("#Id").val();

            if (usid != undefined) {

                $("#selectCat").hide();
            }

            let fullCategoryPropeties = () => {


                if (bind() == undefined) {
                    s.CategoryPropeties = [{
                        PropertyName: "",

                    }];
                }
            }

            s.DrawControls = (category) => {

                $("#tblCatCreate").show();
                $("#tblCatEdit").hide();





                itemsFactory.CreateControls(category.Id).then(function success(response) {


                    s.CategoryPropeties = response.data;


                    s.CategoryPropeties.forEach(function (arr, index) {
                        //  s.propName[index] = "";
                        s.propName[index] = arr.PropertyName;
                        arr.PropertyName = "";

                    })
                    //  s.propName = [];


                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });

            }

            //get all Main Store
            storesFactory.GetAllStore("Main").then(function success(response) {
                let data = response.data;

                s.MainStore_dll = response.data[0];
                s.MainStores = data;
                s.getSubStore();

            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });

            s.getSubStore = () => {
                let mainStoreId = s.MainStore_dll.Id;
                //get all Sub Store
                storesFactory.GetSubStore(mainStoreId).then(function success(response) {

                    //s.SubStores = response.data;
                    //s.SubStore_dll = response.data[0];
                    let data = response.data;

                    data.push({
                        Id: null, StoreName: "Choose Sub Store"
                    });
                    s.SubStores = data;

                    s.SubStore_dll = {
                        Id: null, StoreName: "Choose Sub Store"
                    };
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }
            if (bind() != undefined) {
                itemsFactory.GetById(bind()).then(function success(response) {
                    $("#tblCatCreate").hide();
                    $("#tblCatEdit").show();

                    s.Model = response.data.Model;
                    s.Brand = response.data.Brand;
                    s.SerialNumber = response.data.SerialNumber;
                    s.IsAccessory = response.data.IsAccessory;

                    itemsFactory.LoadStores(response.data.Id).then(function success(response) {

                        var Mainstore = s.MainStores.filter(arr => {
                            if (arr.Id == response.data.MainStore.Id) {

                                return arr;
                            }
                        });
                        s.MainStore_dll = Mainstore[0];

                        var Substore = s.SubStores.filter(arr2 => {
                            if (arr2.Id == response.data.SubStore.Id) {

                                return arr2;
                            }
                        });
                        s.SubStore_dll = Substore[0];

                    });


                    itemsFactory.LoadControls(response.data.Id).then(function success(response) {

                        $("#tblCatCreate").hide();
                        $("#tblCatEdit").show();

                        s.CategoryPropetiesEdit = response.data;

                        s.CategoryPropetiesEdit.forEach(function (arr, index) {


                        })
                        //  s.propName = [];


                    }, function error(error) {
                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });


                }, function error(error) {

                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }


            fullCategoryPropeties();

            s.AddCategoryProperty = function () {
                let flag = true;
                fullData();
                s.CategoryPropeties.forEach(function (arr, index) {

                    if (arr.PropertyName == "" || typeof (arr.PropertyName) == 'undefined') {

                        arr.PropertyName = null;
                        if (arr.IsRequired && arr.PropertyName == null) {

                            n.pushNotification("danger", "Please Enter the required Item  Attributes");

                        }
                        $("#modalDynamicControls").modal('toggle');

                    }

                });
                if (flag) {
                    s.CategoryPropeties.push({
                        PropertyName: "",
                    });

                }


            }



            function Clear() {
                s.Code = "";
                s.Name = "";
                s.Description = "";
                s.Quantity = "";
                s.Model = "";
                s.SerialNumber = "";
                s.Brand = "";
                s.IsAccessory = false;

                // s.DrawControls(s.OCategory_dll);
            }

            // validate form
            function validateForm(isEditMode = false) {
                let flag = false;
                if (s.IsAccessory == true && (s.SerialNumber == null || s.SerialNumber == "" || s.SerialNumber == 'undefined')) {
                    n.pushNotification("danger", "Please Enter The Item Serial Number ");
                    return false;
                }
                if (s.Model == "" || typeof (s.Model) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Item Model");
                    return false;
                }
                if (s.Brand == "" || typeof (s.Brand) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Item Brand");
                    return false;
                }
                var BreakException = {};
                if (s.CategoryPropeties.length != 0) {
                    s.CategoryPropeties.forEach(function (arr, index) {

                        if (arr.IsRequired && (arr.PropertyName == "" || typeof (arr.PropertyName) == 'undefined' || arr.PropertyName == null)) {
                            if (arr.DataType == 2) {
                                arr.PropertyName = false;
                            } else {
                                n.pushNotification("danger", "Please Enter The " + s.propName[index] + "");
                                flag = false;
                                throw BreakException;
                            }
                        } else {
                            if (!flag)
                                flag = true;
                        }


                    });
                } else {
                    flag = true
                }


                return flag;




            }

            s.CreateItem = () => {

                if (validateForm()) {

                    let data = fullData();


                    let item = {
                        SerialNumber: s.SerialNumber,
                        IsAccessory: s.IsAccessory,
                        Model: s.Model,
                        Brand: s.Brand,
                        MainStoreId: s.MainStore_dll.Id,
                        CategoryId: s.OCategory_dll.Id,



                        //status
                    };
                    if (data != null) {
                        item.ItemPropertiesValues = data.PropertiesOfCategory;
                    }
                    if (s.SubStore_dll != null) {
                        item.SubStoreId = s.SubStore_dll.Id;
                    }

                    //if (s.MainStore_dll != null) {
                    //    store.MainStoreId = s.MainStore_dll.Id;
                    //}
                    // get all safes    
                    itemsFactory.CreateItem(item).then(function success(response) {
                        n.pushNotification("success", "Added Successfully");
                        s.CategoryPropeties.forEach(function (arr, index) {

                            arr.PropertyName = "";

                        })
                        Clear();
                    }, function error(error) {

                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });
                }
            };


            function fullData() {
                let flag = true;
                let propties = [];
                s.CategoryPropeties.forEach(function (arr, index) {


                    // check the record not null and have a value
                    if (arr.PropertyName != "" && typeof (arr.PropertyName) != 'undefined') {

                        arr.IsInValid = false;
                        propties.push(arr);
                    }
                    else if (arr.IsRequired && arr.DataType == 2 && (arr.PropertyName == "" || typeof (arr.PropertyName) == 'undefined' || arr.PropertyName == null)) {
                        arr.PropertyName = false;
                        propties.push(arr);
                    }

                });

                if (!flag)
                    return null;
                return {
                    Id: bind(),
                    PropertiesOfCategory: propties
                };
            }
            function fullDataForEdit() {
                let flag = true;
                let propties = [];
                s.CategoryPropetiesEdit.forEach(function (arr, index) {
                    if (arr.PropertyNameValue == null && arr.IsRequired) {
                        flag = false
                        arr.IsInValid = false;

                        n.pushNotification("danger", "Please Enter All category attributes Values");

                    }
                    // check the record not null and have a value
                    if (arr.PropertyNameValue != "" && typeof (arr.PropertyNameValue) != 'undefined') {

                        arr.IsInValid = false;
                        propties.push(arr);
                    } else {
                        // skip last record only is empty 
                        if (s.CategoryPropetiesEdit.length - 1 != index) {
                            arr.IsInValid = true;

                            flag = false;

                        }
                    }
                });
                if (!flag)
                    return null;
                return {
                    Id: bind(),
                    PropertiesOfCategory: propties
                };
            }

            function bind() {
                var usid = $("#Id").val();
                return usid;
            }
            s.EditItem = () => {

                s.CategoryPropetiesEdit.forEach(function (arr, index) {

                    if (arr.IsRequired && (arr.PropertyNameValue == "" || typeof (arr.PropertyNameValue) == 'undefined' || arr.PropertyNameValue == null)) {
                        if (arr.DataType == 2) {
                            arr.PropertyName = false;
                        } else {
                            n.pushNotification("danger", "Please Enter The " + arr.PropertyName + "");
                        }
                    } else {
                    }


                });

                let data = fullDataForEdit();
                if (s.IsAccessory == true && (s.SerialNumber == null || s.SerialNumber == "" || s.SerialNumber == 'undefined')) {
                    n.pushNotification("danger", "Please Enter The Item Serial Number ");
                    return false;
                }
                if (s.Model == "" || typeof (s.Model) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Item Model");
                    return false;
                }
                if (s.Brand == "" || typeof (s.Brand) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Item Brand");
                    return false;
                }
                let item = {
                    SerialNumber: s.SerialNumber,
                    IsAccessory: s.IsAccessory,
                    Model: s.Model,
                    Brand: s.Brand,
                    MainStoreId: s.MainStore_dll.Id,
                    CategoryId: s.OCategory_dll.Id,
                    ItemPropertiesValues: data.PropertiesOfCategory,

                    Id: bind()
                };


                if (data != null) {
                    item.ItemPropertiesValues = data.PropertiesOfCategory;
                }
                if (s.SubStore_dll != null) {
                    item.SubStoreId = s.SubStore_dll.Id;
                }

                itemsFactory.UpdateItem(item).then(function success(response) {
                    n.pushNotification("success", "Updated Successfully");

                }, function error(error) {

                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);


                });


            };


            storesFactory.GetAllBranchs().then(function success(response) {
                let data = response.data;
                s.BranchSearch_dll = {
                    Id: "", Name: "All"
                };
                data.push({
                    Id: "", Name: "All"
                });

                s.BranchsSearch = data;
            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });
            storesFactory.getAllForSearch().then(function success(response) {
                s.Items = [];
                s.Items = response.data;
                dT.pushDataTable("#table_Items");

            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });

            s.Remove = (itemId, index, e) => {
                e.preventDefault();
                bootbox.confirm('Confirm Delete Operation', function (flag) {
                    if (flag) {
                        itemsFactory.RemoveItem(itemId).then(function success(response) {
                            s.Items.splice(index, 1);
                            n.pushNotification("success", "Successfully Deleted");
                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    }
                });
            };
            s.ddl_storeType = "all";

            s.getStoresOfThisBranch = () => {
                s.Items = [];
                s.MainStoresSearch = [];
                s.SubStoresSearch = [];


                let mainBranchId = s.BranchSearch_dll.Id;
                if (mainBranchId == null || mainBranchId == "") {
                    storesFactory.GetAllStoreForSearch("Main").then(function success(response) {
                        let data = response.data;

                        data.push({
                            Id: "", StoreName: "All"
                        });
                        s.MainStoresSearch = data;

                        s.MainStoreSearch_dll = {
                            Id: "", StoreName: "All"
                        };
                    }, function error(error) {
                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });
                } else {
                    //get all Sub Store
                    storesFactory.getStoresOfThisBranch(mainBranchId).then(function success(response) {


                        let data = response.data;

                        data.push({
                            Id: "", StoreName: "All"
                        });
                        s.MainStoresSearch = data;

                        s.MainStoreSearch_dll = {
                            Id: "", StoreName: "All"
                        };
                    }
                        , function error(error) {
                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                }
            }



            storesFactory.GetAllStoreForSearch("Main").then(function success(response) {
                let data = response.data;
                data.push({
                    Id: "", StoreName: "All"
                });
                s.MainStoreSearch_dll = {
                    Id: "", StoreName: "All"
                };
                s.MainStoresSearch = data;
                s.getSubStoreSearch();

            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });
            s.subStoreSearchChange = () => {
                //  s.Items = [];


            }
            s.getSubStoreSearch = () => {
                if (window.location.href.indexOf("Create") > -1) {

                }
                else {

                    let mainStoreId = null;
                    if (s.MainStoreSearch_dll != null && typeof (s.MainStoreSearch_dll) != 'undefined') {

                        mainStoreId = s.MainStoreSearch_dll.Id;

                    }

                    if (mainStoreId == null || mainStoreId == "" || mainStoreId == undefined) {
                        storesFactory.GetAllStoreForSearch("Sub").then(function success(response) {
                            let data = response.data;

                            data.push({
                                Id: "", StoreName: "All"
                            });
                            s.SubStoresSearch = data;

                            s.SubStoreSearch_dll = {
                                Id: "", StoreName: "All"
                            };
                        }, function error(error) {
                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    } else {
                        //get all Sub Store
                        storesFactory.getSubStoreForSearch(mainStoreId).then(function success(response) {


                            let data = response.data;

                            data.push({
                                Id: "", StoreName: "All"
                            });
                            s.SubStoresSearch = data;

                            s.SubStoreSearch_dll = {
                                Id: "", StoreName: "All"
                            };
                        }
                            , function error(error) {
                                let errorMessage = manageAjaxError(error);
                                n.pushNotification("danger", errorMessage);
                            });
                    }
                }
                // s.Items = [];



            }

            s.getItemForSearch = () => {
                s.Items = [];
                s.manageSelectAll = false;
                let type = s.ddl_itemtype;
                itemsFactory.getAllForSearch(type, s.BranchSearch_dll.Id, s.MainStoreSearch_dll.Id, s.SubStoreSearch_dll.Id).then(function success(response) {

                    s.showItems = true;
                    s.Items = response.data;
                    dT.pushDataTable("#table_Items");
                }, function error(error) {
                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }
        }]);


