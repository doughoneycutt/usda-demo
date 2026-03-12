window.cropYieldInterop = {
    ensureCounterInitialized(key, initialValue) {
        const storedValue = window.localStorage.getItem(key);
        const parsedValue = Number.parseInt(storedValue ?? '', 10);

        if (storedValue === null || Number.isNaN(parsedValue)) {
            window.localStorage.setItem(key, String(initialValue));
            return initialValue;
        }

        return parsedValue;
    },

    readCounter(key) {
        return window.cropYieldInterop.ensureCounterInitialized(key, 0);
    },

    incrementCounterAndNotify(key, dotNetHelper) {
        const currentValue = window.cropYieldInterop.ensureCounterInitialized(key, 0);
        const nextValue = currentValue + 1;
        window.localStorage.setItem(key, String(nextValue));
        return dotNetHelper.invokeMethodAsync('SetCounterFromJs', nextValue);
    }
};
