import { useState, useRef } from "react";

export default function useDebounce<T>(
  defaultValue: T,
  timeout = 1000
): [T, (newState: T, bypass?: boolean) => void, boolean] {
  const [state, setState] = useState<T>(defaultValue);
  const [isDebouncing, setIsBouncing] = useState(false);
  const debounceRef = useRef<number | null>();

  function changeState(newState: T, bypass = false) {
    if (debounceRef.current) {
      clearTimeout(debounceRef.current);
    }

    if (bypass) {
      setState(newState);
      debounceRef.current = null;
      setIsBouncing(false);
    } else {
      setIsBouncing(true);
      debounceRef.current = setTimeout(() => {
        setState(newState);
        debounceRef.current = null;
        setIsBouncing(false);
      }, timeout);
    }
  }

  return [state, changeState, isDebouncing];
}
