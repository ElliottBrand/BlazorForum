var ckeditor = null;
var allCkEditors = [];
window.methods = {
    loadEditor: function (TextAreaId) {
        ClassicEditor
            .create(document.querySelector('#' + TextAreaId), {
                link: {
                    addTargetToExternalLinks: true
                }
            })
            .then(editor => {
                ckeditor = editor;
                allCkEditors.push(editor);

                // Show typing alert while user is typing in editor
                let typerId = "";
                editor.editing.view.document.on('keydown', function () {
                    // Call blazor to alert that someone is typing.
                    var topicTypingContainer = document.getElementsByClassName('topic-typing-container');
                    if (topicTypingContainer) {
                        const typerIdPromise = new Promise((resolve, reject) => {
                            resolve(document.getElementById('typerid').value);
                        }).then((result) => {
                            typerId = result;
                        });
                        
                        if (typerId) {
                            console.log(typerId);
                            DotNet.invokeMethodAsync('BlazorForum', 'ShowTypingNotice', topicTypingContainer[0].id, typerId);
                        }
                    }
                });
            })
            .catch(error => {
                console.error(error);
            });
    },
    getEditorText: function () {
        return ckeditor.getData();
    },
    getTargetedEditorText: function (TextAreaId) {
        return ckEditor(TextAreaId).getData();
    },
    clearEditor: function () {
        ckeditor.setData('');
    },
    clearTargetedEditor: function (TextAreaId) {
        ckEditor(TextAreaId).setData('');
    },
    removeTargetedEditor: function (TextAreaId) {
        if (ckEditor(TextAreaId)) {
            ckEditor(TextAreaId).destroy();
            var index = getArrayIndexForKey(allCkEditors, "id", TextAreaId);
            allCkEditors.splice(index, 1);
        }
    }
};

function ckEditor(name) {
    for (var i = 0; i < allCkEditors.length; i++) {
        if (allCkEditors[i].sourceElement.id === name) return allCkEditors[i];
    }
    return null;
}

function getArrayIndexForKey(arr, key, val) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i][key] === val) return i;
    }
    return -1;
}
