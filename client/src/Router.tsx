import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "@/LoginPage.tsx";
import App from "@/App.tsx";

export default function Router() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<LoginPage />} />
                <Route path="/users" element={<App />} />
            </Routes>
        </BrowserRouter>
    )
}