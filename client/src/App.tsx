import {UserList} from "@/UserList.tsx";
import {type User, UserRole} from "@/api/Api.ts";

export function App() {

    const loggedInUser: User = {
        userId: "1",
        userName: "testUser",
        password: "",
        role: UserRole.Admin
    }

    return (
        <div className="app">
            <h1>Kvist og Bjælke</h1>
            <UserList loggedInUser={loggedInUser}/>
        </div>
    );
}

export default App;
