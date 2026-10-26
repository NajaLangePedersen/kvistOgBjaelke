import {type User} from "@/api/Api.ts";
import {useEffect, useState} from "react";
import apiClient from "./ApiClient";

export function UserList() {

    const [users, setUsers] = useState<User[]>([]);

    useEffect(() => {
        apiClient.api.getUsers.userGetUsers().then(r => {
            setUsers(r);
        });
    }, []);

    return (
        <div>
            {users.map(user => (
                <div
                    key={user.userId}
                >
                    {user.userName}
                </div>
            ))}
        </div>
    );
}

