import {useState, type FormEvent} from "react";
import apiClient from "./ApiClient";
import {
    type UpdateRequestDTO,
    type User,
    UserRole,
} from "@/api/Api.ts";

export function EditUserForm({
                                 user, loggedInUser, onCancel, onSaved,
                             }: {
    user: User;
    loggedInUser: User;
    onCancel: () => void;
    onSaved: (changes: {
        userName: string;
        role: UserRole;
    }) => void;
}) {
    const [userName, setUserName] = useState(user.userName);
    const [newPassword, setNewPassword] = useState("");
    const [role, setRole] = useState(user.role);
    const [errorMessage, setErrorMessage] = useState("");

    const canChangeRole = loggedInUser.role === UserRole.Admin;

    async function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();
        setErrorMessage("");

        const trimmedUserName = userName.trim();

        if (!trimmedUserName) {
            setErrorMessage("Username cannot be empty");
            return;
        }

        const request: UpdateRequestDTO = {
            userId: user.userId,
            userName: trimmedUserName,
            ...(newPassword.trim() ? {newPassword} : {}),
            ...(canChangeRole ? {role} : {}),
        };

        try {
            await apiClient.api.api.userUpdate(request);

            onSaved({
                userName: trimmedUserName,
                role: canChangeRole ? role : user.role,
            });
        } catch {
            setErrorMessage("User could not be updated");
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <h3>Edit {user.userName}</h3>

            <label>
                Username
                <input
                    value={userName}
                    onChange={event => setUserName(event.target.value)}
                    required
                />
            </label>

            <label>
                New password
                <input
                    type="password"
                    value={newPassword}
                    placeholder="Leave blank to keep the current password"
                    onChange={event => setNewPassword(event.target.value)}
                />
            </label>

            {canChangeRole && (
                <label>
                    Role
                    <select
                        value={role}
                        onChange={event =>
                            setRole(Number(event.target.value) as UserRole)
                        }
                    >
                        <option value={UserRole.User}>User</option>
                        <option value={UserRole.SuperUser}>SuperUser</option>
                        <option value={UserRole.Admin}>Admin</option>
                    </select>
                </label>
            )}

            {errorMessage && <p>{errorMessage}</p>}

            <button type="submit">Save</button>
            <button type="button" onClick={onCancel}>
                Cancel
            </button>
        </form>
    );
}