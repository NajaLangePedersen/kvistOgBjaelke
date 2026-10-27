import {UserList} from "@/UserList.tsx";
import {type User} from "@/api/Api.ts";
import {useState} from "react";

export function App() {

    const [loggedInUser] = useState<User | null>(() => {
        const savedUser = localStorage.getItem("loggedInUser");

        if (!savedUser) {
            return null;
        }

        try {
            return JSON.parse(savedUser) as User;
        } catch {
            return null;
        }
    });

    return (
        <div className="app">
            <h1>Kvist og Bjælke</h1>

            {loggedInUser ? (
                <UserList loggedInUser={loggedInUser} />
            ) : (
                <p>Du er ikke logget ind.</p>
            )}

        </div>
    );
}

export default App;
