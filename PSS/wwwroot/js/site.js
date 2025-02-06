async function getHandle() {
    // set some options, like the suggested file name and the file type.
    const options = {
        suggestedName: 'Teklif.pdf',
        types: [
            {
                description: 'PDF Files',
                accept: {
                    'text/plain': ['.pdf'],
                },
            },
        ],
    };

    // prompt the user for the location to save the file.
    const handle = await window.showSaveFilePicker(options);

    return handle.name
}