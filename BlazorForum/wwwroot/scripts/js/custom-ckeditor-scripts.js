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

                // Show icon while user is typing in editor
                editor.editing.view.document.on('keydown', function () {
                    // Call blazor to alert that someone is typing.
                    // ** Need to fix this to hide the alert for the user who is typing ** //
                    var topicTypingContainer = document.getElementsByClassName('topic-typing-container');
                    if (topicTypingContainer) {
                        const typerIdPromise = new Promise((resolve, reject) => {
                            resolve(DotNet.invokeMethodAsync('BlazorForum', 'GetTyperId'));
                        })

                        let typerId = "test";
                        typerIdPromise.then((result) => {
                            typerId = result;
                        })
                        console.log(typerId);
                        // Isn't currently sending typerId to Blazor. Value isn't being set inside Promise 'then' statement
                        DotNet.invokeMethodAsync('BlazorForum', 'ShowTypingNotice', topicTypingContainer[0].id, typerId);
                    }
                });
            })
            .catch(error => {
                console.error(error);
            });
    },
    typingDelay: function () {

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
