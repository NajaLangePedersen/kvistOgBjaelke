import {useState} from "react";
import { useNavigate } from "react-router-dom";
import apiClient from "@/ApiClient.ts";
import {getUserFromToken} from "@/auth/getUserFromToken.ts";

export default function LoginPage(){
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");

        try {

            const response = await apiClient.api.api.userLogin({
                username,
                password,
            });

            const token = response.token;

            // Gem tokenet, så vi kan bruge det til API-kald.
            localStorage.setItem("token", token);

            apiClient.api.setSecurityData(token);

            // Hent brugerens oplysninger fra tokenet.
            const user = getUserFromToken(token);

            if (!user) {
                localStorage.removeItem("token");
                setError("Could not read user information from login token");
                return;
            }

            // Gem brugeren, så App.tsx kan hente den.
            localStorage.setItem("loggedInUser", JSON.stringify(user));

            navigate("/users");

        } catch (err) {
            setError("Login failed");
        }
    }

    return (
        <form onSubmit={handleLogin}>
            <input
                type="text"
                placeholder="Username"
                value={username}
                onChange={(e) =>
                    setUsername(e.target.value)}
            />

            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) =>
                    setPassword(e.target.value)}
            />

            <button type="submit">Login</button>

            {error && <p>{error}</p>}
        </form>
    );
}