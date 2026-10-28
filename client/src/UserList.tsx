import {type User, UserRole} from "@/api/Api.ts";
import {useEffect, useState} from "react";
import apiClient from "./ApiClient";
import {CreateUserForm} from "@/CreateUserForm.tsx";
import {EditUserForm} from "@/EditUserForm.tsx";

function canManageRole(actorRole: UserRole, targetRole: UserRole): boolean {
    return actorRole === UserRole.Admin ||
        (actorRole === UserRole.SuperUser && targetRole === UserRole.User);
}

export function UserList({loggedInUser}: {
    loggedInUser: User;
}) {
    const [editingUser, setEditingUser] = useState<User | null>(null);
    const [users, setUsers] = useState<User[]>([]);
    const [errorMessage, setErrorMessage] = useState("");


    useEffect(() => {
        apiClient.api.api.userGetUsers().then(r => {
            setUsers(r);
        });
    }, []);

    async function handleDelete(user: User) {
        const confirmed = window.confirm(`Do u want to delete ${user.userName}?`);
        if (!confirmed) return;

        try {
            await apiClient.api.api.userDelete({
                id: user.userId,
            });
            setUsers(currentUser =>
                currentUser.filter(currentUser => currentUser.userId !== user.userId)
            );
        } catch (err) {
            setErrorMessage("User could not be deleted");
        }
    }

    return (
        <div>

            {loggedInUser.role !== UserRole.User && (
                <CreateUserForm
                    setUsers={setUsers}
                    loggedInUser={loggedInUser}
                />
            )}

            {users.map(user => (
                <div key={user.userId}>
                    {user.userName}

                    {canManageRole(loggedInUser.role, user.role) && (
                        <button onClick={() => handleDelete(user)}>
                            Delete User
                        </button>
                    )}

                    {canManageRole(loggedInUser.role, user.role) && (
                        <button onClick={() => setEditingUser(user)}>
                            Edit
                        </button>
                    )}
                </div>
            ))}

            {editingUser && (
                <EditUserForm
                    user={editingUser}
                    loggedInUser={loggedInUser}
                    onCancel={() => setEditingUser(null)}
                    onSaved={(changes) => {
                        setUsers(current =>
                            current.map(user =>
                                user.userId === editingUser.userId
                                    ? {...user, ...changes}
                                    : user
                            )
                        );
                        setEditingUser(null);
                    }}
                />
            )}
        </div>
    );

}




