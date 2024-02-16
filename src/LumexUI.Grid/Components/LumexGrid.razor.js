export function init(element, navigable) {
    const clickHandler = event => {
        const editCell = element.querySelector('.lumex-grid-cell.lumex-edit');

        if (editCell) {
            if (event.composedPath().indexOf(editCell) < 0) {
                element.dispatchEvent(new CustomEvent('clickoutside', { bubbles: true }));
            }
        }
    };

    const keyDownHandler = event => {
        if (navigable) {
            const editCell = element.querySelector('.lumex-grid-cell.lumex-edit');

            if (editCell && event.key == "Tab") {
                event.preventDefault();
            }

            element.addEventListener('keydown', keyDownHandler);
        };
    }

    document.body.addEventListener('click', clickHandler);

    return {
        destroy: () => {
            document.body.removeEventListener('click', clickHandler);

            if (navigable) {
                element.removeEventListener('keydown', keyDownHandler);
            }
        }
    };
}