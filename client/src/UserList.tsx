import {type User, UserRole} from "@/api/Api.ts";
import {useEffect, useState} from "react";
import apiClient from "./ApiClient";
import {CreateUserForm} from "@/CreateUserForm.tsx";

export function UserList({loggedInUser}: {
    loggedInUser: User;
}) {

    const [users, setUsers] = useState<User[]>([]);

    useEffect(() => {
        apiClient.api.getUsers.userGetUsers().then(r => {
            setUsers(r);
        });
    }, []);

    return (
        <div>

            {loggedInUser.role !== UserRole.User && (
                <CreateUserForm
                    setUsers={setUsers}
                    loggedInUser={loggedInUser}
                />
            )}

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

