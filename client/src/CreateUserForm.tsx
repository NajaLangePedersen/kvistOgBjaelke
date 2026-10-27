import {type Dispatch, type SetStateAction, useState} from "react";
import apiClient from "./ApiClient";
import {type User, UserRole} from "@/api/Api.ts";

export function CreateUserForm({setUsers, loggedInUser}: {
    setUsers: Dispatch<SetStateAction<User[]>>;
    loggedInUser: User;
}) {

    const [userNameField, setUserNameField] = useState("");
    const [passwordField, setPasswordField] = useState("");
    const [role, setRole] = useState<UserRole>(UserRole.User);

    function handleCreateUser() {
        apiClient.api.userCreateUser({
            username: userNameField,
            password: passwordField,
            role: role
        })
            .then(() => setUserNameField(""))
            .then(() => setPasswordField(""))
            .then(() => setRole(UserRole.User))
            .then(() => {
                apiClient.api.userGetUsers().then(r => {
                    setUsers(r);
                })
            })
    }

    return (
        <div>
            Create User:
            <input
                placeholder={"username"}
                value={userNameField}
                onChange={(e) => setUserNameField(e.target.value)}
            />
            <input
                type={"password"}
                placeholder={"password"}
                value={passwordField}
                onChange={e => setPasswordField(e.target.value)}
            />
            <select
                value={role}
                onChange={(e) => setRole(Number(e.target.value) as UserRole)}
            >
                <option value={UserRole.User}>User</option>

                <option
                    value={UserRole.SuperUser}
                    disabled={loggedInUser.role !== UserRole.Admin}
                >
                    SuperUser
                </option>

                <option
                    value={UserRole.Admin}
                    disabled={loggedInUser.role !== UserRole.Admin}
                >
                    Admin
                </option>
            </select>

            <button
                onClick={handleCreateUser}
            >
                Save
            </button>

        </div>
    )

}